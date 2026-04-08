using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Teams.API.Features.Projects;
using Teams.API.Features.Projects.AddTeamToProject;
using Teams.API.Features.Projects.CreateProject;
using Teams.API.Features.Projects.EditProject;
using Teams.API.Features.Projects.GetAllProjects;
using Teams.API.Features.Projects.GetAllTeamMembershipRequests;
using Teams.API.Features.Projects.RequestToJoinTeam;
using Teams.API.Features.Requests;
using Teams.API.Features.Roles;
using Teams.API.Features.Skills;
using Teams.API.Features.Users;
using Teams.API.Logging;
using Teams.API.Services;
using Teams.API.Validation;
using Teams.Domain.Aggregates.ProjectAggregate;
using Teams.Domain.Aggregates.UserAggregate;
using Teams.Domain.SharedKernel;
using Teams.Infrastructure;

namespace Teams.API.Extensions;

internal static class Extensions
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {
        var services = builder.Services;

        builder.Services.AddAuthentication("Bearer")
            .AddJwtBearer("Bearer", options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["Jwt:ValidAudience"],
                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration["Jwt:ValidIssuer"],
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecurityKey"]!))
                };
            });

        builder.Services.AddDbContext<TeamDbContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            options.UseSeeding((context, _) =>
            {
                var java = context.Set<Skill>()
                    .FirstOrDefault(s => s.Name == "Java");
                if (java == null)
                {
                    java = new Skill("Java");
                    context.Set<Skill>().Add(java);
                    context.SaveChanges();
                }
                
                var js = context.Set<Skill>()
                    .FirstOrDefault(s => s.Name == "JavaScript");
                if (js == null)
                {
                    js = new Skill("JavaScript");
                    context.Set<Skill>().Add(js);
                    context.SaveChanges();
                }
                
                var tsSkill = context.Set<Skill>()
                    .FirstOrDefault(s => s.Name == "TypeScript");
                if (tsSkill == null)
                {
                    tsSkill = new Skill("TypeScript");
                    context.Set<Skill>().Add(tsSkill);
                    context.SaveChanges();
                }
                
                var pySkill = context.Set<Skill>()
                    .FirstOrDefault(s => s.Name == "Python");
                if (pySkill == null)
                {
                    pySkill = new Skill("Python");
                    context.Set<Skill>().Add(pySkill);
                    context.SaveChanges();
                }
                
                var cplusplusSkill = context.Set<Skill>()
                    .FirstOrDefault(s => s.Name == "C++");
                if (cplusplusSkill == null)
                {
                    cplusplusSkill = new Skill("C++");
                    context.Set<Skill>().Add(cplusplusSkill);
                    context.SaveChanges();
                }
                
                var sqlSkill = context.Set<Skill>()
                    .FirstOrDefault(s => s.Name == "SQL");
                if (sqlSkill == null)
                {
                    sqlSkill = new Skill("SQL");
                    context.Set<Skill>().Add(sqlSkill);
                    context.SaveChanges();
                }
                
                var react = context.Set<Skill>()
                    .FirstOrDefault(s => s.Name == "React");
                if (react == null)
                {
                    react = new Skill("React");
                    context.Set<Skill>().Add(react);
                    context.SaveChanges();
                }
                
                var angularSkill = context.Set<Skill>()
                    .FirstOrDefault(s => s.Name == "Angular");
                if (angularSkill == null)
                {
                    angularSkill = new Skill("Angular");
                    context.Set<Skill>().Add(angularSkill);
                    context.SaveChanges();
                }
                
                var expressSkill = context.Set<Skill>()
                    .FirstOrDefault(s => s.Name == "Express");
                if (expressSkill == null)
                {
                    expressSkill = new Skill("Express");
                    context.Set<Skill>().Add(expressSkill);
                    context.SaveChanges();
                }
                
                var springSkill = context.Set<Skill>()
                    .FirstOrDefault(s => s.Name == "Spring");
                if (springSkill == null)
                {
                    springSkill = new Skill("Spring");
                    context.Set<Skill>().Add(springSkill);
                    context.SaveChanges();
                }
                
                var flaskSkill = context.Set<Skill>()
                    .FirstOrDefault(s => s.Name == "Flask");
                if (flaskSkill == null)
                {
                    flaskSkill = new Skill("Flask");
                    context.Set<Skill>().Add(flaskSkill);
                    context.SaveChanges();
                }
                
                var dockerSkill = context.Set<Skill>()
                    .FirstOrDefault(s => s.Name == "Docker");
                if (dockerSkill == null)
                {
                    dockerSkill = new Skill("Docker");
                    context.Set<Skill>().Add(dockerSkill);
                    context.SaveChanges();
                }
                
                var kubernetesSkill = context.Set<Skill>()
                    .FirstOrDefault(s => s.Name == "Kubernetes");
                if (kubernetesSkill == null)
                {
                    kubernetesSkill = new Skill("Kubernetes");
                    context.Set<Skill>().Add(kubernetesSkill);
                    context.SaveChanges();
                }
                
                var rabbitmqSkill = context.Set<Skill>()
                    .FirstOrDefault(s => s.Name == "RabbitMQ");
                if (rabbitmqSkill == null)
                {
                    rabbitmqSkill = new Skill("RabbitMQ");
                    context.Set<Skill>().Add(rabbitmqSkill);
                    context.SaveChanges();
                }
                
                var kafkaSkill = context.Set<Skill>()
                    .FirstOrDefault(s => s.Name == "Kafka");
                if (kafkaSkill == null)
                {
                    kafkaSkill = new Skill("Kafka");
                    context.Set<Skill>().Add(kafkaSkill);
                    context.SaveChanges();
                }
                
                var pulsarSkill = context.Set<Skill>()
                    .FirstOrDefault(s => s.Name == "Apache Pulsar");
                if (pulsarSkill == null)
                {
                    pulsarSkill = new Skill("Apache Pulsar");
                    context.Set<Skill>().Add(pulsarSkill);
                    context.SaveChanges();
                }
                
                var elasticsearchSkill = context.Set<Skill>()
                    .FirstOrDefault(s => s.Name == "Elasticsearch");
                if (elasticsearchSkill == null)
                {
                    elasticsearchSkill = new Skill("Elasticsearch");
                    context.Set<Skill>().Add(elasticsearchSkill);
                    context.SaveChanges();
                }
                
                var mysqlSkill = context.Set<Skill>()
                    .FirstOrDefault(s => s.Name == "MySQL");
                if (mysqlSkill == null)
                {
                    mysqlSkill = new Skill("MySQL");
                    context.Set<Skill>().Add(mysqlSkill);
                    context.SaveChanges();
                }
                
                var sqlServerSkill = context.Set<Skill>()
                    .FirstOrDefault(s => s.Name == "Microsoft SQL Server");
                if (sqlServerSkill == null)
                {
                    sqlServerSkill = new Skill("Microsoft SQL Server");
                    context.Set<Skill>().Add(sqlServerSkill);
                    context.SaveChanges();
                }
                
                var dynamoDbSkill = context.Set<Skill>()
                    .FirstOrDefault(s => s.Name == "DynamoDB");
                if (dynamoDbSkill == null)
                {
                    dynamoDbSkill = new Skill("DynamoDB");
                    context.Set<Skill>().Add(dynamoDbSkill);
                    context.SaveChanges();
                }
                
                var awsSkill =  context.Set<Skill>()
                    .FirstOrDefault(s => s.Name == "AWS");
                if (awsSkill == null)
                {
                    awsSkill = new Skill("AWS");
                    context.Set<Skill>().Add(awsSkill);
                    context.SaveChanges();
                }
                
                var azureSkill = context.Set<Skill>()
                    .FirstOrDefault(s => s.Name == "Azure");
                if (azureSkill == null)
                {
                    azureSkill = new Skill("Azure");
                    context.Set<Skill>().Add(azureSkill);
                    context.SaveChanges();
                }
                
                var azureDevOpsSkill = context.Set<Skill>()
                    .FirstOrDefault(s => s.Name == "Azure DevOps");
                if (azureDevOpsSkill == null)
                {
                    azureDevOpsSkill = new Skill("Azure DevOps");
                    context.Set<Skill>().Add(azureDevOpsSkill);
                    context.SaveChanges();
                }
                
                var pyTorchSkill  = context.Set<Skill>()
                    .FirstOrDefault(s => s.Name == "PyTorch");
                if (pyTorchSkill == null)
                {
                    pyTorchSkill = new Skill("PyTorch");
                    context.Set<Skill>().Add(pyTorchSkill);
                    context.SaveChanges();
                }
                
                var openCvSkill =  context.Set<Skill>()
                    .FirstOrDefault(s => s.Name == "OpenCV");
                if (openCvSkill == null)
                {
                    openCvSkill = new Skill("OpenCV");
                    context.Set<Skill>().Add(openCvSkill);
                    context.SaveChanges();
                }
                
                var solderingSkill = context.Set<Skill>()
                    .FirstOrDefault(s => s.Name == "Soldering");
                if (solderingSkill == null)
                {
                    solderingSkill = new Skill("Soldering");
                    context.Set<Skill>().Add(solderingSkill);
                    context.SaveChanges();
                }
                
                var plcSkill = context.Set<Skill>()
                    .FirstOrDefault(s => s.Name == "PLC");
                if (plcSkill == null)
                {
                    plcSkill = new Skill("PLC");
                    context.Set<Skill>().Add(plcSkill);
                    context.SaveChanges();
                }

                var unitySkill = context.Set<Skill>()
                    .FirstOrDefault(s => s.Name == "Unity");
                if (unitySkill == null)
                {
                    unitySkill = new Skill("Unity");
                    context.Set<Skill>().Add(unitySkill);
                    context.SaveChanges();
                }

                var fullstackRole = context.Set<Role>()
                    .FirstOrDefault(role => role.Name == "Fullstack");
                if (fullstackRole == null)
                {
                    fullstackRole = new Role("Fullstack");
                    context.Set<Role>().Add(fullstackRole);
                    context.SaveChanges();
                }
                
                var frontendRole = context.Set<Role>()
                    .FirstOrDefault(r => r.Name == "Frontend");
                if (frontendRole == null)
                {
                    frontendRole = new Role("Frontend");
                    context.Set<Role>().Add(frontendRole);
                    context.SaveChanges();
                }
                
                var backendRole = context.Set<Role>()
                    .FirstOrDefault(r => r.Name == "Backend");
                if (backendRole == null)
                {
                    backendRole = new Role("Backend");
                    context.Set<Role>().Add(backendRole);
                    context.SaveChanges();
                }

                var embeddedRole = context.Set<Role>()
                    .FirstOrDefault(r => r.Name == "Embedded");
                if (embeddedRole == null)
                {
                    embeddedRole = new Role("Embedded");
                    context.Set<Role>().Add(embeddedRole);
                    context.SaveChanges();
                }
                
                var machineLearningRole = context.Set<Role>()
                    .FirstOrDefault(r => r.Name == "Machine Learning");
                if (machineLearningRole == null)
                {
                    machineLearningRole = new Role("Machine Learning");
                    context.Set<Role>().Add(machineLearningRole);
                    context.SaveChanges();
                }

                // Film roles.
                var fxArtistRole = context.Set<Role>()
                    .FirstOrDefault(r => r.Name == "FX Artist");
                if (fxArtistRole == null)
                {
                    fxArtistRole = new Role("FX Artist");
                    context.Set<Role>().Add(fxArtistRole);
                    context.SaveChanges();
                }

                // game design roles
                var threeDModelingRole = context.Set<Role>()
                    .FirstOrDefault(r => r.Name == "3D Modeler");
                if (threeDModelingRole == null)
                {
                    threeDModelingRole = new Role("3D Modeler");
                    context.Set<Role>().Add(threeDModelingRole);
                    context.SaveChanges();
                }

                var textureArtistRole = context.Set<Role>()
                    .FirstOrDefault(r => r.Name == "Texture artist");
                if (textureArtistRole == null)
                {
                    textureArtistRole = new Role("Texture artist");
                    context.Set<Role>().Add(textureArtistRole);
                    context.SaveChanges();
                }

                var programmerRole = context.Set<Role>()
                    .FirstOrDefault(r => r.Name == "Programmer");
                if (programmerRole == null)
                {
                    programmerRole = new Role("Programmer");
                    context.Set<Role>().Add(programmerRole);
                    context.SaveChanges();
                }

                var mechanicsDesignerRole = context.Set<Role>()
                    .FirstOrDefault(r => r.Name == "Mechanics Designer");
                if (mechanicsDesignerRole == null)
                {
                    mechanicsDesignerRole = new Role("Mechanics Designer");
                    context.Set<Role>().Add(mechanicsDesignerRole);
                    context.SaveChanges();
                }

                var levelDesignerRole = context.Set<Role>()
                    .FirstOrDefault(r => r.Name == "Level Designer");
                if (levelDesignerRole == null)
                {
                    levelDesignerRole = new Role("Level Designer");
                    context.Set<Role>().Add(levelDesignerRole);
                    context.SaveChanges();
                }

                // business roles
                var financeRole = context.Set<Role>()
                    .FirstOrDefault(r => r.Name == "Finances");
                if (financeRole == null)
                {
                    financeRole = new Role("Finances");
                    context.Set<Role>().Add(financeRole);
                    context.SaveChanges();
                }

                var baRole = context.Set<Role>()
                    .FirstOrDefault(r => r.Name == "Business Analyst");
                if (baRole == null)
                {
                    baRole = new Role("Business Analyst");
                    context.Set<Role>().Add(baRole);
                    context.SaveChanges();
                }

                var accountingRole = context.Set<Role>()
                    .FirstOrDefault(r => r.Name == "Accountant");
                if (accountingRole == null)
                {
                    accountingRole = new Role("Accountant");
                    context.Set<Role>().Add(accountingRole);
                    context.SaveChanges();
                }

                var mgmtRole = context.Set<Role>()
                    .FirstOrDefault(r => r.Name == "Manager");
                if (mgmtRole == null)
                {
                    mgmtRole = new Role("Manager");
                    context.Set<Role>().Add(mgmtRole);
                    context.SaveChanges();
                }

                var user = context.Set<User>()
                    .FirstOrDefault(m => m.IdentityGuid == builder.Configuration["SeedUsers:0:IdentityGuid"]);
                if (user == null)
                {
                    user = new User(Guid.CreateVersion7(), "MataTeams", "Admin", false, builder.Configuration["SeedUsers:0:IdentityGuid"]!);
                    user.AddSkill(java);
                    context.Set<User>().Add(user);
                    context.SaveChanges();
                }
                
                // Dabbles in frontend development but also really likes embedded work.
                var demoUser = context.Set<User>()
                    .FirstOrDefault(u => u.IdentityGuid == builder.Configuration["SeedUsers:1:IdentityGuid"]);
                if (demoUser == null)
                {
                    demoUser = new User(Guid.CreateVersion7(), "Matty", "Miller", false, builder.Configuration["SeedUsers:1:IdentityGuid"]!);
                    demoUser.AddSkill(js);
                    demoUser.AddSkill(react);
                    demoUser.AddSkill(pySkill);
                    demoUser.AddSkill(cplusplusSkill);
                    demoUser.AddSkill(solderingSkill);
                    context.Set<User>().Add(demoUser);
                    context.SaveChanges();
                }

                var user3 = context.Set<User>()
                    .FirstOrDefault(u => u.IdentityGuid == "00000000000000000000000000000003");
                if (user3 == null)
                {
                    user3 = new User(Guid.CreateVersion7(), "Patty", "Palmer", false, "00000000000000000000000000000003");
                    user3.AddSkill(js);
                    user3.AddSkill(react);
                    user3.AddSkill(expressSkill);
                    context.Set<User>().Add(user3);
                    context.SaveChanges();
                }

                var user4 = context.Set<User>()
                    .FirstOrDefault(u => u.IdentityGuid == "00000000000000000000000000000004");
                if (user4 == null)
                {
                    user4 = new User(Guid.CreateVersion7(), "Maria", "Martinez", false, "00000000000000000000000000000004");
                    user4.AddSkill(cplusplusSkill);
                    user4.AddSkill(solderingSkill);
                    context.Set<User>().Add(user4);
                    context.SaveChanges();
                }

                var user5 = context.Set<User>()
                    .FirstOrDefault(u => u.IdentityGuid == "00000000000000000000000000000005");
                if (user5 == null)
                {
                    user5 = new User(Guid.CreateVersion7(), "Maria", "Martinez", false, "00000000000000000000000000000005");
                    context.Set<User>().Add(user5);
                    context.SaveChanges();
                }

                var project = context.Set<Project>()
                    .FirstOrDefault(p => p.Name == "ARCS: RecyCOOL");
                if (project == null)
                {
                    project = new Project(
                        "ARCS: RecyCOOL",
                        "GOAL: Increase recycling participation and food separation rates " +
                        "in communities while decreasing contamination in waste bins. " +
                        "Create applications in support of this goal, conduct site visits " +
                        "to locations for research, and collaborate with community leaders.",
                        ProjectType.FromName("ARCS"),
                        ProjectStatus.Draft,
                        user.Id,
                        true,
                        null);
                    // Add `Frontend` Role with `JavaScript` and `React` Skills.
                    var projectRole = project.AddProjectRole(frontendRole.Id, 2, [js, react]);
                    // Add `Backend` Role with `Java` Skill.
                    project.AddProjectRole(backendRole.Id, 2, [pySkill]);
                    context.Set<Project>().Add(project);
                    var team = project.AddTeamToProject("Students", project.OwnerId);

                    context.SaveChanges();
                }

                var project2 = context.Set<Project>()
                    .FirstOrDefault(p => p.Name == "Chess Club App");
                if (project2 == null)
                {
                    project2 = new Project(
                        "Chess Club App",
                        "A collaborative app for the CSUN Chess Club, which will " +
                        "allow members to draft and solve puzzles for tournaments. " +
                        "We aim to release on the Google Play store in late 2026.",
                        ProjectType.FromName("Club"),
                        ProjectStatus.Planning,
                        user3.Id,
                        false,
                        null
                    );
                    var projectRole1 = project2.AddProjectRole(frontendRole.Id, 1, [react, js, expressSkill]);
                    var projectRole2 = project2.AddProjectRole(fullstackRole.Id, 1, [java, sqlSkill]);

                    context.Set<Project>().Add(project2);
                    context.SaveChanges();
                }

                var project3 = context.Set<Project>()
                    .FirstOrDefault(p => p.Name == "Retro Game Console");
                if (project3 == null)
                {
                    project3 = new Project(
                        "Retro Game Console",
                        "A personal project to recreate classic arcade games. " +
                        "We'll be soldering components onto a custom board, " +
                        "including buttons and the display.",
                        ProjectType.FromName("Personal"),
                        ProjectStatus.Planning,
                        user4.Id,
                        false,
                        null);
                    var projectRole = project3.AddProjectRole(embeddedRole.Id, 3, [cplusplusSkill, solderingSkill]);

                    context.Set<Project>().Add(project3);
                    var team = project3.AddTeamToProject("Team 1", project3.OwnerId);
                    var request = project3.AddTeamMembershipRequest(team.Id, project3.OwnerId, projectRole.Id);
                    project3.RespondToMembershipRequest(project3.OwnerId, request.Id, TeamMembershipRequestStatus.Approved);

                    context.SaveChanges();
                }

                var project4 = context.Set<Project>()
                    .FirstOrDefault(p => p.Name == "Prosthetic Arm");
                if (project4 == null)
                {
                    project4 = new Project(
                        "Prosthetic Arm",
                        "The Robotics Club is working on a proesthetic arm. " +
                        "We're processing input signals and writing code to " +
                        "manage motor control. We have microcontrollers, 3D-printed parts, " +
                        "and wiring. Beginners welcome!",
                        ProjectType.FromName("Club"),
                        ProjectStatus.Planning,
                        user4.Id,
                        false,
                        null);
                    var projectRole = project4.AddProjectRole(embeddedRole.Id, 4, [cplusplusSkill, solderingSkill]);
                    context.Set<Project>().Add(project4);

                    var team = project4.AddTeamToProject("Robotics Club", project4.OwnerId);
                    var request = project4.AddTeamMembershipRequest(team.Id, project4.OwnerId, projectRole.Id);
                    project4.RespondToMembershipRequest(project4.OwnerId, request.Id, TeamMembershipRequestStatus.Approved);

                    context.SaveChanges();
                }

                var project5 = context.Set<Project>()
                    .FirstOrDefault(p => p.Name == "New MataSync");
                if (project5 == null)
                {
                    project5 = new Project(
                        "New MataSync",
                        "I want to put together a team to enroll into COMP 490, " +
                        "any Mo/We section works for me. " + 
                        "I'm thinking to build a new MataSync to organize clubs. " +
                        "I've already talked to several organizations on campus " +
                        "and have the feature set and user flow figured out.",
                        ProjectType.FromName("Class"),
                        ProjectStatus.Planning,
                        user3.Id,
                        false,
                        null);
                    var projectRole = project5.AddProjectRole(fullstackRole.Id, 2, [js, react, sqlSkill]);
                    context.Set<Project>().Add(project5);

                    var team = project5.AddTeamToProject("COMP 490 Group", project5.OwnerId);
                    var request = project5.AddTeamMembershipRequest(team.Id, project5.OwnerId, projectRole.Id);
                    project5.RespondToMembershipRequest(project5.OwnerId, request.Id, TeamMembershipRequestStatus.Approved);

                    context.SaveChanges();
                }

                var project6 = context.Set<Project>()
                    .FirstOrDefault(p => p.Name == "Vocabulary Game");
                if (project6 == null)
                {
                    project6 = new Project(
                        "Vocabulary Game",
                        "I'm building a ReactJS app to teach elementary school kids " +
                        "new vocabulary, as part of my volunteering for the local school. " +
                        "Looking for one other person who wants to help build this!",
                        ProjectType.FromName("Personal"),
                        ProjectStatus.Planning,
                        user3.Id,
                        false,
                        null);
                    var projectRole = project6.AddProjectRole(fullstackRole.Id, 2, [js, react, expressSkill]);
                    context.Set<Project>().Add(project6);

                    var team = project6.AddTeamToProject("Pair Sharing", project6.OwnerId);
                    var request = project6.AddTeamMembershipRequest(team.Id, project6.OwnerId, projectRole.Id);
                    project6.RespondToMembershipRequest(project6.OwnerId, request.Id, TeamMembershipRequestStatus.Approved);

                    context.SaveChanges();
                }

                var project7 = context.Set<Project>()
                    .FirstOrDefault(p => p.Name == "Professor: Computer Vision Rover");
                if (project7 == null)
                {
                    project7 = new Project(
                        "Professor: Computer Vision Rover",
                        "Dr. Schwartz is looking for motivated students " +
                        "to help build a rover, leveraging computer vision " +
                        "to identity animals on campus. The camera is to " +
                        "be mounted on a wheeled platform, controlable remotely. " +
                        "Past experience is helpful but not a hard requirement.",
                        ProjectType.FromName("Faculty"),
                        ProjectStatus.Planning,
                        user.Id,
                        false,
                        null);

                    var projectRole1 = project7.AddProjectRole(embeddedRole.Id, 2, [cplusplusSkill, solderingSkill]);
                    var projectRole2 = project7.AddProjectRole(fullstackRole.Id, 2, [pySkill, openCvSkill]);
                    var team = project7.AddTeamToProject("Research Assistants", project7.OwnerId);

                    context.Set<Project>().Add(project7);
                    context.SaveChanges();
                }

                var project8 = context.Set<Project>()
                    .FirstOrDefault(p => p.Name == "CSUN Parking Tracker Tool");
                if (project8 == null)
                {
                    project8 = new Project(
                        "CSUN Parking Tracker Tool",
                        "Tool to analyze trends in parking structure occupancy, " +
                        "using the publicly-available information in the CSUN App.",
                        ProjectType.FromName("Personal"),
                        ProjectStatus.Planning,
                        user.Id,
                        false,
                        null);

                    var projectRole = project8.AddProjectRole(fullstackRole.Id, 2, [js, react, expressSkill]);

                    context.Set<Project>().Add(project8);
                    context.SaveChanges();
                }

                var project9 = context.Set<Project>()
                    .FirstOrDefault(p => p.Name == "Physics Simulation Software");
                if (project9 == null)
                {
                    project9 = new Project(
                        "Physics Simulation Software",
                        "The Physics department is looking for students in Computer Science " +
                        "to assist in the creation of physics simulation software. Full details " +
                        "can be found at https://www.csun.edu/science-mathematics/physics-astronomy",
                        ProjectType.FromName("Faculty"),
                        ProjectStatus.Planning,
                        user.Id,
                        false,
                        null);

                    var projectRole = project9.AddProjectRole(fullstackRole.Id, 2, [cplusplusSkill]);

                    context.Set<Project>().Add(project9);
                    context.SaveChanges();
                }

                var project10 = context.Set<Project>()
                    .FirstOrDefault(p => p.Name == "Mental Health Platform for Hackathon");
                if (project10 == null)
                {
                    project10 = new Project(
                        "Mental Health Platform for Hackathon",
                        "For MataHacks 2026, I want to build an application to collate " +
                        "local resources for counseling, mediatation, and exercise. " +
                        "I haven't finalized the feature set yet, but I know that I " +
                        "want to work in the stack I know best.",
                        ProjectType.FromName("Personal"),
                        ProjectStatus.Draft,
                        user5.Id,
                        false,
                        null);

                    var projectRole = project8.AddProjectRole(fullstackRole.Id, 2, [js, react, expressSkill, sqlSkill]);

                    context.Set<Project>().Add(project10);
                    context.SaveChanges();
                }

                var project11 = context.Set<Project>()
                    .FirstOrDefault(p => p.Name == "Android App");
                if (project11 == null)
                {
                    project11 = new Project(
                        "Android App",
                        "...",
                        ProjectType.FromName("Personal"),
                        ProjectStatus.Draft,
                        user5.Id,
                        false,
                        null);

                    project11.AddProjectRole(backendRole.Id, 2, [java]);

                    context.Set<Project>().Add(project11);
                    context.SaveChanges();
                }

                var project12 = context.Set<Project>()
                    .FirstOrDefault(p => p.Name == "SEO Audit Tool");
                if (project12 == null)
                {
                    project12 = new Project(
                        "SEO Audit Tool",
                        "...",
                        ProjectType.FromName("Personal"),
                        ProjectStatus.Draft,
                        user5.Id,
                        false,
                        null);
                    project12.AddProjectRole(fullstackRole.Id, 2, [java]);

                    context.Set<Project>().Add(project12);
                    context.SaveChanges();
                }

                var project13 = context.Set<Project>()
                    .FirstOrDefault(p => p.Name == "Database Engine in Java");
                if (project13 == null)
                {
                    project13 = new Project(
                        "Database Engine in Java",
                        "...",
                        ProjectType.FromName("Personal"),
                        ProjectStatus.Draft,
                        user5.Id,
                        false,
                        null);
                    project13.AddProjectRole(backendRole.Id, 2, [java, sqlSkill]);

                    context.Set<Project>().Add(project13);
                    context.SaveChanges();
                }

                var project14 = context.Set<Project>()
                    .FirstOrDefault(p => p.Name == "Kubernetes Clone");
                if (project14 == null)
                {
                    project14 = new Project(
                        "Kubernetes Clone",
                        "...",
                        ProjectType.FromName("Personal"),
                        ProjectStatus.Draft,
                        user5.Id,
                        false,
                        null);
                    project14.AddProjectRole(backendRole.Id, 2, [java]);

                    context.Set<Project>().Add(project14);
                    context.SaveChanges();
                }

                var project15 = context.Set<Project>()
                    .FirstOrDefault(p => p.Name == "CLI AWS Resource Monitor");
                if (project15 == null)
                {
                    project15 = new Project(
                        "CLI AWS Resource Monitor",
                        "...",
                        ProjectType.FromName("Personal"),
                        ProjectStatus.Draft,
                        user5.Id,
                        false,
                        null);
                    project15.AddProjectRole(backendRole.Id, 2, [java, sqlSkill]);

                    context.Set<Project>().Add(project15);
                    context.SaveChanges();
                }

                var project16 = context.Set<Project>()
                    .FirstOrDefault(p => p.Name == "8-bit Music Player");
                if (project16 == null)
                {
                    project16 = new Project(
                        "8-bit Music Player",
                        "...",
                        ProjectType.FromName("Personal"),
                        ProjectStatus.Draft,
                        user5.Id,
                        false,
                        null);
                    project16.AddProjectRole(backendRole.Id, 2, [java]);

                    context.Set<Project>().Add(project16);
                    context.SaveChanges();
                }

                var project17 = context.Set<Project>()
                    .FirstOrDefault(p => p.Name == "AR Furniture Rearranger");
                if (project17 == null)
                {
                    project17 = new Project(
                        "AR Furniture Rearranger",
                        "...",
                        ProjectType.FromName("Personal"),
                        ProjectStatus.Draft,
                        user5.Id,
                        false,
                        null);
                    project17.AddProjectRole(backendRole.Id, 2, [unitySkill]);

                    context.Set<Project>().Add(project17);
                    context.SaveChanges();
                }

                var project18 = context.Set<Project>()
                    .FirstOrDefault(p => p.Name == "Augmented Reality Makeup App");
                if (project18 == null)
                {
                    project18 = new Project(
                        "Augmented Reality Makeup App",
                        "...",
                        ProjectType.FromName("Personal"),
                        ProjectStatus.Draft,
                        user5.Id,
                        false,
                        null);
                    project18.AddProjectRole(backendRole.Id, 2, [unitySkill]);

                    context.Set<Project>().Add(project18);
                    context.SaveChanges();
                }

                var gameProject1 = context.Set<Project>()
                    .FirstOrDefault(p => p.Name == "Isometric Base Builder");
                if (gameProject1 == null)
                {
                    gameProject1 = new Project(
                        "Isometric Base Builder",
                        "...",
                        ProjectType.FromName("Game Design"),
                        ProjectStatus.Alpha,
                        user5.Id,
                        false,
                        null);

                    gameProject1.AddProjectRole(threeDModelingRole.Id, 1, []);

                    context.Set<Project>().Add(gameProject1);
                    context.SaveChanges();
                }

                var filmProject1 = context.Set<Project>()
                     .FirstOrDefault(p => p.Name == "Sci-Fi Student Film");
                if (filmProject1 == null)
                {
                    filmProject1 = new Project(
                        "Sci-Fi Student Film",
                        "The CSUN Film Club has a student film in the post-production phase. " +
                        "The current FX artists on the project are graduating this summer. " +
                        "We're looking for student volunteers to carry the project through!",
                        ProjectType.FromName("Film"),
                        ProjectStatus.PostProduction,
                        user5.Id,
                        false,
                        null);

                    filmProject1.AddProjectRole(fxArtistRole.Id, 2, [unitySkill]);

                    context.Set<Project>().Add(filmProject1);
                    context.SaveChanges();
                }

                var projectCopy1 = context.Set<Project>()
                    .FirstOrDefault(p => p.Name == "RecyCOOL Team Proposal");
                if (projectCopy1 == null)
                {
                    projectCopy1 = new Project(
                        "RecyCOOL Team Proposal",
                        "Putting together an ARCS team for a COMP 490 Mo/We section. " + "\n" +
                        "\n" +
                        "GOAL: Increase recycling participation and food separation rates " +
                        "in communities while decreasing contamination in waste bins. " +
                        "Create applications in support of this goal, conduct site visits " +
                        "to locations for research, and collaborate with community leaders.",
                        ProjectType.FromName("ARCS"),
                        ProjectStatus.Draft,
                        user3.Id,
                        false,
                        project.Id);
                    var projectRole = projectCopy1.AddProjectRole(frontendRole.Id, 2, [js, react]);
                    projectCopy1.AddProjectRole(backendRole.Id, 2, [pySkill]);
                    context.Set<Project>().Add(projectCopy1);
                    var team = projectCopy1.AddTeamToProject("Students", projectCopy1.OwnerId);

                    var request = projectCopy1.AddTeamMembershipRequest(team.Id, projectCopy1.OwnerId, projectRole.Id);
                    projectCopy1.RespondToMembershipRequest(projectCopy1.OwnerId, request.Id, TeamMembershipRequestStatus.Approved);

                    context.SaveChanges();
                }

                var projectCopy2 = context.Set<Project>()
                    .FirstOrDefault(p => p.Name == "RecyCOOL Team");
                if (projectCopy2 == null)
                {
                    projectCopy2 = new Project(
                        "RecyCOOL Team",
                        "GOAL: Increase recycling participation and food separation rates " +
                        "in communities while decreasing contamination in waste bins. " +
                        "Create applications in support of this goal, conduct site visits " +
                        "to locations for research, and collaborate with community leaders.",
                        ProjectType.FromName("ARCS"),
                        ProjectStatus.Draft,
                        user4.Id,
                        false,
                        project.Id);
                    projectCopy2.AddProjectRole(frontendRole.Id, 2, [js, react]);
                    var projectRole = projectCopy2.AddProjectRole(backendRole.Id, 2, [pySkill]);
                    context.Set<Project>().Add(projectCopy2);
                    var team = projectCopy2.AddTeamToProject("Students", projectCopy2.OwnerId);

                    var request = projectCopy2.AddTeamMembershipRequest(team.Id, projectCopy1.OwnerId, projectRole.Id);
                    projectCopy2.RespondToMembershipRequest(projectCopy2.OwnerId, request.Id, TeamMembershipRequestStatus.Approved);

                    context.SaveChanges();
                }

                // Excluded from recommendations for demoUser.
                var projectDemoUserIsLeader = context.Set<Project>()
                    .FirstOrDefault(p => p.Name == "Energy Saving App");
                if (projectDemoUserIsLeader == null)
                {
                    projectDemoUserIsLeader = new Project(
                        "Energy Saving App",
                        "A web application to simulate energy savings from various changes. " +
                        "For example, switching to LED lighting or improving insulation. " +
                        "We'll visualize savings in real time and track progress against goals.",
                        ProjectType.FromName("Personal"),
                        ProjectStatus.Active,
                        demoUser.Id,
                        false,
                        null);
                    var projectRole = projectDemoUserIsLeader.AddProjectRole(fullstackRole.Id, 2, [js, react]);
                    context.Set<Project>().Add(projectDemoUserIsLeader);

                    var team = projectDemoUserIsLeader.AddTeamToProject("Fullstack", projectDemoUserIsLeader.OwnerId);
                    var request = projectDemoUserIsLeader.AddTeamMembershipRequest(team.Id, projectDemoUserIsLeader.OwnerId, projectRole.Id);
                    projectDemoUserIsLeader.RespondToMembershipRequest(projectDemoUserIsLeader.OwnerId, request.Id, TeamMembershipRequestStatus.Approved);

                    context.SaveChanges();
                }

                // Excluded from recommendations for demoUser.
                var projectDemoUserIsMember = context.Set<Project>()
                    .FirstOrDefault(p => p.Name == "LADWP Automatic Water Pump Controller");
                if (projectDemoUserIsMember == null)
                {
                    projectDemoUserIsMember = new Project(
                        "LADWP Automatic Water Pump Controller",
                        "In association with the LADWP, the CSUN Department of Electrical & Computer Engineering " +
                        "is building a prototype to explore the next generation of water distribution control. " +
                        "The work will involve sensors to monitor water levels in tanks and eliminate manual monitoring. " +
                        "Full details can be found at https://www.csun.edu/engineering-computer-science/electrical-computer-engineering",
                        ProjectType.FromName("Faculty"),
                        ProjectStatus.Active,
                        user.Id,
                        false,
                        null);
                    var projectRole = projectDemoUserIsMember.AddProjectRole(embeddedRole.Id, 3, [cplusplusSkill, solderingSkill]);
                    context.Set<Project>().Add(projectDemoUserIsMember);

                    var team = projectDemoUserIsMember.AddTeamToProject("Fullstack", projectDemoUserIsMember.OwnerId);
                    var request = projectDemoUserIsMember.AddTeamMembershipRequest(team.Id, demoUser.Id, projectRole.Id);
                    projectDemoUserIsMember.RespondToMembershipRequest(projectDemoUserIsMember.OwnerId, request.Id, TeamMembershipRequestStatus.Approved);

                    context.SaveChanges();
                }

                var recommendation = context.Set<Recommendation>()
                    .SingleOrDefault(r => r.Project.Name == "ARCS: RecyCOOL");
                if (recommendation is null)
                {
                    recommendation = new Recommendation
                    {
                        User = demoUser,
                        Project = project,
                        MatchPercentage = new decimal(0.99)
                    };
                    context.Set<Recommendation>().Add(recommendation);
                    context.SaveChanges();
                }

                var recommendation2 = context.Set<Recommendation>()
                    .SingleOrDefault(r => r.Project.Name == "Chess Club App");
                if (recommendation2 is null)
                {
                    recommendation2 = new Recommendation
                    {
                        User = demoUser,
                        Project = project2,
                        MatchPercentage = new decimal(0.98)
                    };
                    context.Set<Recommendation>().Add(recommendation2);
                    context.SaveChanges();
                }

                var recommendation3 = context.Set<Recommendation>()
                    .SingleOrDefault(r => r.Project.Name == "Retro Game Console");
                if (recommendation3 is null)
                {
                    recommendation3 = new Recommendation
                    {
                        User = demoUser,
                        Project = project3,
                        MatchPercentage = new decimal(0.97)
                    };
                    context.Set<Recommendation>().Add(recommendation3);
                    context.SaveChanges();
                }

                var recommendation4 = context.Set<Recommendation>()
                    .SingleOrDefault(r => r.Project.Name == "Prosthetic Arm");
                if (recommendation4 is null)
                {
                    recommendation4 = new Recommendation
                    {
                        User = demoUser,
                        Project = project4,
                        MatchPercentage = new decimal(0.96)
                    };
                    context.Set<Recommendation>().Add(recommendation4);
                    context.SaveChanges();
                }

                var recommendation5 = context.Set<Recommendation>()
                    .SingleOrDefault(r => r.Project.Name == "New MataSync");
                if (recommendation5 is null)
                {
                    recommendation5 = new Recommendation
                    {
                        User = demoUser,
                        Project = project5,
                        MatchPercentage = new decimal(0.95)
                    };
                    context.Set<Recommendation>().Add(recommendation5);
                    context.SaveChanges();
                }

                var recommendation6 = context.Set<Recommendation>()
                    .SingleOrDefault(r => r.Project.Name == "Vocabulary Game");
                if (recommendation6 is null)
                {
                    recommendation6 = new Recommendation
                    {
                        User = demoUser,
                        Project = project6,
                        MatchPercentage = new decimal(0.94)
                    };
                    context.Set<Recommendation>().Add(recommendation6);
                    context.SaveChanges();
                }

                var recommendation7 = context.Set<Recommendation>()
                    .SingleOrDefault(r => r.Project.Name == "Professor: Computer Vision Rover");
                if (recommendation7 is null)
                {
                    recommendation7 = new Recommendation
                    {
                        User = demoUser,
                        Project = project7,
                        MatchPercentage = new decimal(0.93)
                    };
                    context.Set<Recommendation>().Add(recommendation7);
                    context.SaveChanges();
                }

                var recommendation8 = context.Set<Recommendation>()
                    .SingleOrDefault(r => r.Project.Name == "Professor: Computer Vision Rover");
                if (recommendation8 is null)
                {
                    recommendation8 = new Recommendation
                    {
                        User = demoUser,
                        Project = project8,
                        MatchPercentage = new decimal(0.92)
                    };
                    context.Set<Recommendation>().Add(recommendation8);
                    context.SaveChanges();
                }

                var recommendation9 = context.Set<Recommendation>()
                    .SingleOrDefault(r => r.Project.Name == "Physics Simulation Software");
                if (recommendation9 is null)
                {
                    recommendation9 = new Recommendation
                    {
                        User = demoUser,
                        Project = project9,
                        MatchPercentage = new decimal(0.91)
                    };
                    context.Set<Recommendation>().Add(recommendation9);
                    context.SaveChanges();
                }

                var recommendation10 = context.Set<Recommendation>()
                    .SingleOrDefault(r => r.Project.Name == "Mental Health Platform for Hackathon");
                if (recommendation10 is null)
                {
                    recommendation10 = new Recommendation
                    {
                        User = demoUser,
                        Project = project10,
                        MatchPercentage = new decimal(0.90)
                    };
                    context.Set<Recommendation>().Add(recommendation10);
                    context.SaveChanges();
                }

                var recommendation11 = context.Set<Recommendation>()
                    .SingleOrDefault(r => r.Project.Name == "Android App");
                if (recommendation11 is null)
                {
                    recommendation11 = new Recommendation
                    {
                        User = demoUser,
                        Project = project11,
                        MatchPercentage = new decimal(0.20)
                    };
                    context.Set<Recommendation>().Add(recommendation11);
                    context.SaveChanges();
                }

                var recommendation12 = context.Set<Recommendation>()
                    .SingleOrDefault(r => r.Project.Name == "SEO Audit Tool");
                if (recommendation12 is null)
                {
                    recommendation12 = new Recommendation
                    {
                        User = demoUser,
                        Project = project12,
                        MatchPercentage = new decimal(0.19)
                    };
                    context.Set<Recommendation>().Add(recommendation12);
                    context.SaveChanges();
                }

                var recommendation13 = context.Set<Recommendation>()
                    .SingleOrDefault(r => r.Project.Name == "Database Engine in Java");
                if (recommendation13 is null)
                {
                    recommendation13 = new Recommendation
                    {
                        User = demoUser,
                        Project = project13,
                        MatchPercentage = new decimal(0.18)
                    };
                    context.Set<Recommendation>().Add(recommendation13);
                    context.SaveChanges();
                }

                var recommendation14 = context.Set<Recommendation>()
                    .SingleOrDefault(r => r.Project.Name == "Kubernetes Clone");
                if (recommendation14 is null)
                {
                    recommendation14 = new Recommendation
                    {
                        User = demoUser,
                        Project = project14,
                        MatchPercentage = new decimal(0.17)
                    };
                    context.Set<Recommendation>().Add(recommendation14);
                    context.SaveChanges();
                }

                var recommendation15 = context.Set<Recommendation>()
                    .SingleOrDefault(r => r.Project.Name == "CLI AWS Resource Monitor");
                if (recommendation15 is null)
                {
                    recommendation15 = new Recommendation
                    {
                        User = demoUser,
                        Project = project15,
                        MatchPercentage = new decimal(0.16)
                    };
                    context.Set<Recommendation>().Add(recommendation15);
                    context.SaveChanges();
                }

                var recommendation16 = context.Set<Recommendation>()
                    .SingleOrDefault(r => r.Project.Name == "8-bit Music Player");
                if (recommendation16 is null)
                {
                    recommendation16 = new Recommendation
                    {
                        User = demoUser,
                        Project = project16,
                        MatchPercentage = new decimal(0.15)
                    };
                    context.Set<Recommendation>().Add(recommendation16);
                    context.SaveChanges();
                }

                var recommendation17 = context.Set<Recommendation>()
                    .SingleOrDefault(r => r.Project.Name == "AR Furniture Rearranger");
                if (recommendation17 is null)
                {
                    recommendation17 = new Recommendation
                    {
                        User = demoUser,
                        Project = project17,
                        MatchPercentage = new decimal(0.14)
                    };
                    context.Set<Recommendation>().Add(recommendation17);
                    context.SaveChanges();
                }

                var recommendation18 = context.Set<Recommendation>()
                    .SingleOrDefault(r => r.Project.Name == "Augmented Reality Makeup App");
                if (recommendation18 is null)
                {
                    recommendation18 = new Recommendation
                    {
                        User = demoUser,
                        Project = project18,
                        MatchPercentage = new decimal(0.13)
                    };
                    context.Set<Recommendation>().Add(recommendation18);
                    context.SaveChanges();
                }

                var recommendationGame1 = context.Set<Recommendation>()
                    .SingleOrDefault(r => r.Project.Name == "Isometric Base Builder");
                if (recommendationGame1 is null)
                {
                    recommendationGame1 = new Recommendation
                    {
                        User = demoUser,
                        Project = gameProject1,
                        MatchPercentage = new decimal(0.05)
                    };
                    context.Set<Recommendation>().Add(recommendationGame1);
                    context.SaveChanges();
                }

                var recommendationFilm1 = context.Set<Recommendation>()
                    .SingleOrDefault(r => r.Project.Name == "Sci-Fi Student Film");
                if (recommendationFilm1 is null)
                {
                    recommendationFilm1 = new Recommendation
                    {
                        User = demoUser,
                        Project = filmProject1,
                        MatchPercentage = new decimal(0.03)
                    };
                    context.Set<Recommendation>().Add(recommendationFilm1);
                    context.SaveChanges();
                }
            });
        });
        
        builder.Services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblyContaining<Program>();
            
            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
            cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
        });

        builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
        
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddTransient<IIdentityService, IdentityService>();
        builder.Services.AddSingleton<IAuthorizationHandler, ProjectIsOwnerAuthorizationHandler>();
        builder.Services.AddSingleton<IAuthorizationHandler, TeamIsLeaderAuthorizationHandler>();
    }

    public static void MapEndpoints(this IEndpointRouteBuilder app)
    {
        var projectsGroup = app.MapGroup("/api/projects").WithTags("Projects");
        var teamsGroup = app.MapGroup("/api/teams").WithTags("Project Teams");
        var requestsGroup = app.MapGroup("/api/requests").WithTags("Team Requests");
        var skillsGroup = app.MapGroup("/api/skills").WithTags("Skills");
        var rolesGroup = app.MapGroup("/api/roles").WithTags("Roles");
        
        GetProjectById.MapEndpoint(projectsGroup);
        GetAllProjectsEndpoint.Map(projectsGroup);
        CreateProject.MapEndpoint(projectsGroup);
        EditProject.Map(projectsGroup);
        AddTeamToProjectEndpoint.Map(projectsGroup);
        RequestToJoinTeam.MapEndpoint(teamsGroup);
        GetAllTeamMembershipRequests.MapEndpoint(teamsGroup);
        GetTeamByIdEndpoint.Map(teamsGroup);
        RespondToMembershipRequest.MapEndpoint(requestsGroup);
        
        GetSkillsEndpoint.Map(skillsGroup);
        GetRolesEndpoint.Map(rolesGroup);
        
        var usersGroup = app.MapGroup("/api/users").WithTags("Users");
        CreateProfile.MapEndpoint(usersGroup);
        GetAuthenticatedUserProfileEndpoint.Map(usersGroup);
        EditAuthenticatedUserProfile.MapEndpoint(usersGroup);
        GetProfileByIdEndpoint.Map(usersGroup);
        GetUserTeamsAndRolesEndpoint.Map(usersGroup);
        GetUserMembershipRequestsEndpoint.Map(usersGroup);
        GetRecommendationsEndpoint.Map(usersGroup);
    }
}