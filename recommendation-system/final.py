import asyncio
import aio_pika
import json
from os import getenv
from dotenv import load_dotenv
from mssql_python import connect
import uuid
from typing import Dict, List, Set
from dataclasses import dataclass
from datetime import datetime

@dataclass
class UserProfile:
    user_id: str
    identity_guid: str
    first_name: str
    last_name: str
    is_faculty_or_staff: bool
    programs: List[str]
    skills: Set[str]
    last_updated: datetime

@dataclass
class Project:
    project_id: str
    name: str
    description: str
    type: str
    status: str
    owner_id: str
    required_skills: Set[str]

class RecommendationEngine:
    def __init__(self):
        self.users: Dict[str, UserProfile] = {}
        self.projects: Dict[str, Project] = {}

    #users
    def add_user(self, user_data: dict, source: str = "fetched"):
        user_id = user_data.get("userId")
        if not user_id:
            return
        
        self.users[user_id] = UserProfile(
            user_id=user_id,
            identity_guid=user_data.get("identityGuid", ""),
            first_name=user_data.get("firstName", ""),
            last_name=user_data.get("lastName", ""),
            is_faculty_or_staff=user_data.get("isFacultyOrStaff", False),
            programs=user_data.get("programNames", []),
            skills=set(user_data.get("skillIds", [])),
            last_updated=datetime.now()
        )

        print(f"\n User profile received → {self.users[user_id].first_name} {self.users[user_id].last_name}")
        print(f"   Skills: {list(self.users[user_id].skills)}")
        
        # Print their recommended projects immediately
        self.show_recommendations_for_user(user_id)

    #projects
    def add_project(self, project_data: dict):
        project_id = project_data.get("projectId")
        if not project_id:
            return

        skills = project_data.get("skills", []) or project_data.get("requiredSkillIds", [])

        self.projects[project_id] = Project(
            project_id=project_id,
            name=project_data.get("name", ""),
            description=project_data.get("description", ""),
            type=project_data.get("type", ""),
            status=project_data.get("status", ""),
            owner_id=project_data.get("ownerId", ""),
            required_skills=set(skills)
        )

        print(f"\n Project created → {self.projects[project_id].name}")
        print(f"   Required skills: {list(set(skills))}")

        # Print matching users immediately
        self.show_recommendations_for_project(project_id)

    #rec for users
    def show_recommendations_for_user(self, user_id: str, top_n: int = 10):
        if user_id not in self.users:
            return
        
        user = self.users[user_id]

        if not self.projects:
            print("   No projects exist yet.")
            return

        matches = []
        for project in self.projects.values():

            # Don't recommend to the project owner
            if project.owner_id == user_id:
                continue

            common_skills = user.skills.intersection(project.required_skills)
            if project.required_skills:
                match_score = len(common_skills) / len(project.required_skills)
                if match_score > 0:
                    matches.append((project, match_score, common_skills))

        if not matches:
            print(f"   No matching projects for {user.first_name}")
            return

        matches.sort(key=lambda x: x[1], reverse=True)

        # define SQL Server connection
        load_dotenv()
        conn = connect(getenv("SQL_CONNECTION_STRING"))

        print(f"\n Recommended Projects for {user.first_name} {user.last_name}:")
        for idx, (project, score, common) in enumerate(matches[:top_n], 1):
            print(f"   {idx}. {project.name}  ({score:.0%} match)")
            print(f"      Matching Skills → {list(common)}")
            
            # update the recommendation (project/user pairing) if exists,
            # add it otherwise
            SQL_QUERY = """
            MERGE INTO Recommendations AS target
            USING (VALUES (?, ?, ?, ?, ?)) AS source (Id, UserId, ProjectId, MatchPercentage, ModifiedDate)
              ON target.UserId = source.UserId AND target.ProjectId = source.ProjectId
            WHEN MATCHED THEN
                UPDATE SET 
                    target.MatchPercentage = source.MatchPercentage,
                    target.ModifiedDate = source.ModifiedDate
            WHEN NOT MATCHED THEN
                INSERT (Id, UserId, ProjectId, MatchPercentage, ModifiedDate)
                VALUES (source.Id, source.UserId, source.ProjectId, source.MatchPercentage, source.ModifiedDate);
            """

            cursor = conn.cursor()
            cursor.execute(SQL_QUERY, (uuid.uuid4(), user_id, project.project_id, score, datetime.now()))
            conn.commit()

        # close SQL Server connection
        cursor.close()
        conn.close()

    #rec for proj
    def show_recommendations_for_project(self, project_id: str, top_n: int = 3):
        if project_id not in self.projects:
            return
        
        project = self.projects[project_id]

        if not self.users:
            print("   No users exist yet.")
            return

        matches = []
        for user in self.users.values():

            if user.user_id == project.owner_id:
                continue

            common_skills = user.skills.intersection(project.required_skills)
            if project.required_skills:
                match_score = len(common_skills) / len(project.required_skills)
                if match_score > 0:
                    matches.append((user, match_score, common_skills))

        if not matches:
            print(f"   No matching users for project '{project.name}'")
            return

        matches.sort(key=lambda x: x[1], reverse=True)

        print(f"\n Recommended Users for Project '{project.name}':")
        for idx, (user, score, common) in enumerate(matches[:top_n], 1):
            print(f"   {idx}. {user.first_name} {user.last_name}  ({score:.0%} match)")
            print(f"      Matching Skills → {list(common)}")


engine = RecommendationEngine()

async def setup_consumer():
    """Simple setup that works like your test script"""
    RABBITMQ_URL = "amqp://guest:guest@localhost/"
    
    print(" Recommendation Engine")
    print("="*60)
    
    connection = await aio_pika.connect(RABBITMQ_URL)
    channel = await connection.channel()
    
    # User Profile Exchange
    print("\n Setting up User Profile consumer...")
    user_exchange = await channel.declare_exchange(
        "Teams.Contracts:UserProfileFetched",
        aio_pika.ExchangeType.FANOUT,
        durable=True
    )
    user_queue = await channel.declare_queue("recommendation-user-profile", durable=True)
    await user_queue.bind(user_exchange)
    
    async def handle_user_message(message):
        async with message.process():
            data = json.loads(message.body.decode())
            user_data = data.get("message", {})
            engine.add_user(user_data, "fetched")
    
    await user_queue.consume(handle_user_message)
    print(" Listening for user profiles")
    
    # Project Exchange
    print("\n Setting up Project consumer...")
    project_exchange = await channel.declare_exchange(
        "Teams.Contracts:ProjectCreated",
        aio_pika.ExchangeType.FANOUT,
        durable=True
    )
    project_queue = await channel.declare_queue("recommendation-project-created", durable=True)
    await project_queue.bind(project_exchange)
    
    async def handle_project_message(message):
        async with message.process():
            data = json.loads(message.body.decode())
            project_data = data.get("message", {})
            engine.add_project(project_data)
    
    await project_queue.consume(handle_project_message)
    print(" Listening for projects")
    
    print(f"\n{'='*60}")
    print(" Engine Ready!")
    print("="*60)
    print("\n Current stats:")
    print(f"   Users: {len(engine.users)}")
    print(f"   Projects: {len(engine.projects)}")
    print("\n Make requests to:")
    print("   • GET /api/users/me → Shows user skills & recommendations")
    print("   • POST /api/projects → Shows project details & recommendations")
    print("="*60 + "\n")
    
    print(" Waiting for messages... (Ctrl+C to exit)")
    
    await asyncio.Future()

if __name__ == "__main__":
    try:
        asyncio.run(setup_consumer())
    except KeyboardInterrupt:
        print("\n Shutting down...")