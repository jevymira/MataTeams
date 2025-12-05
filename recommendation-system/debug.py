# simple_async_consumer.py
import asyncio
import aio_pika
import json

async def main():
    RABBITMQ_URL = "amqp://guest:guest@localhost/"
    
    print(" Simple Async Consumer")
    print("="*60)
    
    connection = await aio_pika.connect(RABBITMQ_URL)
    async with connection:
        channel = await connection.channel()
        
        # User Profile Exchange
        user_exchange = "Teams.Contracts:UserProfileFetched"
        user_queue = "python-user-profile"
        
        # Project Exchange  
        project_exchange = "Teams.Contracts:ProjectCreated"
        project_queue = "python-project-created"
        
        print(f"\n Setting up User Profile consumer...")
        
        # Setup User Profile Exchange
        exchange = await channel.declare_exchange(
            user_exchange,
            aio_pika.ExchangeType.FANOUT,
            durable=True
        )
        
        # Create queue
        queue = await channel.declare_queue(user_queue, durable=True)
        
        # Bind queue to exchange
        await queue.bind(exchange)
        
        async def user_callback(message):
            async with message.process():
                data = json.loads(message.body.decode())
                user = data.get("message", {})
                
                print(f"\n{'='*60}")
                print(" USER PROFILE FETCHED")
                print(f"   Name: {user.get('firstName')} {user.get('lastName')}")
                print(f"   Faculty/Staff: {user.get('isFacultyOrStaff')}")
                print(f"   Programs: {user.get('programNames')}")
                
                skills = user.get('skillIds', [])
                print(f"   Skills ({len(skills)}):")
                for skill in skills:
                    print(f"     • {skill}")
                
                print(f"   Fetched: {user.get('fetchedAt')}")
                print("="*60)
        
        await queue.consume(user_callback)
        print(f" Listening for user profiles on {user_exchange}")
        
        print(f"\n Setting up Project consumer...")
        
        # Setup Project Exchange
        exchange = await channel.declare_exchange(
            project_exchange,
            aio_pika.ExchangeType.FANOUT,
            durable=True
        )
        
        # Create queue
        queue = await channel.declare_queue(project_queue, durable=True)
        
        # Bind queue to exchange
        await queue.bind(exchange)
        
        async def project_callback(message):
            async with message.process():
                data = json.loads(message.body.decode())
                project = data.get("message", {})
                
                print(f"\n{'='*60}")
                print(" PROJECT CREATED")
                print(f"   Name: {project.get('name')}")
                print(f"   Description: {project.get('description')}")
                print(f"   Type: {project.get('type')}")
                print(f"   Status: {project.get('status')}")
                
                skills = project.get('skills', [])
                print(f"   Required Skills ({len(skills)}):")
                for skill in skills:
                    print(f"     • {skill}")
                
                print("="*60)
        
        await queue.consume(project_callback)
        print(f" Listening for projects on {project_exchange}")
        
        print(f"\n{'='*60}")
        print(" Consumers Ready!")
        print("="*60)
        print("\n Make requests to:")
        print("   • GET /api/users/me → Shows user skills")
        print("   • POST /api/projects → Shows project details")
        print("="*60 + "\n")
        
        print(" Waiting for messages... (Ctrl+C to exit)")
        
        await asyncio.Future()

if __name__ == "__main__":
    try:
        asyncio.run(main())
    except KeyboardInterrupt:
        print("\n Shutting down...")