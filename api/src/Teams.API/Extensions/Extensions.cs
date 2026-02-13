using System.Security.Claims;
using System.Text;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
using Teams.Domain.SharedKernel;
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
                
                var user = context.Set<User>()
                    .FirstOrDefault(m => m.IdentityGuid == builder.Configuration["SeedUsers:0:IdentityGuid"]);
                if (user == null)
                {
                    user = new User(Guid.CreateVersion7(), "First", "Last", false, builder.Configuration["SeedUsers:0:IdentityGuid"]!);
                    user.AddSkill(js);
                    context.Set<User>().Add(user);
                    context.SaveChanges();
                }
                
                var user2 =  context.Set<User>()
                    .FirstOrDefault(u => u.IdentityGuid == builder.Configuration["SeedUsers:1:IdentityGuid"]);
                if (user2 == null)
                {
                    user2 = new User(Guid.CreateVersion7(), "First", "Last", false, builder.Configuration["SeedUsers:1:IdentityGuid"]!);
                    user2.AddSkill(java);
                    context.Set<User>().Add(user2);
                    context.SaveChanges();
                }
                
                var project = context.Set<Project>()
                    .FirstOrDefault(p => p.Name == "Sample Project");
                if (project == null)
                {
                    project = new Project(
                        "Sample Project",
                        "Sample Text.",
                        ProjectType.FromName("ARCS"),
                        ProjectStatus.Draft,
                        user.Id);
                    // Add `Frontend` Role with `JavaScript` and `React` Skills.
                    var projectRole = project.AddProjectRole(frontendRole.Id, 2, [js, react]);
                    // Add `Backend` Role with `Java` Skill.
                    project.AddProjectRole(backendRole.Id, 2, [java]);
                    context.Set<Project>().Add(project);
                    var team = project.AddTeamToProject("Sample Team", project.OwnerId);
                    var request = project.AddTeamMembershipRequest(team.Id, project.OwnerId, projectRole.Id);
                    project.RespondToMembershipRequest(project.OwnerId, request.Id, TeamMembershipRequestStatus.Approved);
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
        CreateProjectEndpoint.Map(projectsGroup);
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
        EditAuthenticatedUserProfileEndpoint.Map(usersGroup);
        GetProfileByIdEndpoint.Map(usersGroup);
        GetUserTeamsAndRolesEndpoint.Map(usersGroup);
        GetUserMembershipRequestsEndpoint.Map(usersGroup);
        GetRecommendationsEndpoint.Map(usersGroup);
    }
}