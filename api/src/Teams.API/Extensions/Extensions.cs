using System.Text;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Teams.API.Features.Projects;
using Teams.API.Features.Projects.AddTeamToProject;
using Teams.API.Features.Projects.CreateProject;
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
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["Jwt:ValidAudience"],
                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration["Jwt:ValidIssuer"],
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecurityKey"]!))
                });

        builder.Services.AddDbContext<TeamDbContext>(options =>
        {
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
            options.UseSnakeCaseNamingConvention();
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
                
                var react = context.Set<Skill>()
                    .FirstOrDefault(s => s.Name == "React");
                if (react == null)
                {
                    react = new Skill("React");
                    context.Set<Skill>().Add(react);
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
                    project.AddProjectRole(frontendRole.Id, 2);
                    project.Roles.First().AddProjectSkill(js);
                    project.Roles.First().AddProjectSkill(react);
                    // Add `Backend` Role with `Java` Skill.
                    project.AddProjectRole(backendRole.Id, 2);
                    project.Roles.Last().AddProjectSkill(java);
                    context.Set<Project>().Add(project);
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
    }

    public static void MapEndpoints(this IEndpointRouteBuilder app)
    {
        var projectsGroup = app.MapGroup("/api/projects").WithTags("Projects");
        var teamsGroup = app.MapGroup("/api/teams").WithTags("Project Teams");
        var requestsGroup = app.MapGroup("/api/requests").WithTags("Team Requests");
        var skillsGroup = app.MapGroup("/api/skills").WithTags("Skills");
        var rolesGroup = app.MapGroup("/api/roles").WithTags("Roles");
        
        GetProjectById.MapEndpoint(projectsGroup); 
        CreateProjectEndpoint.Map(projectsGroup);
        AddTeamToProjectEndpoint.Map(projectsGroup);
        RequestToJoinTeam.MapEndpoint(teamsGroup);
        GetAllTeamMembershipRequests.MapEndpoint(teamsGroup);
        RespondToMembershipRequest.MapEndpoint(requestsGroup);
        
        GetSkillsEndpoint.Map(skillsGroup);
        GetRolesEndpoint.Map(rolesGroup);
        
        var usersGroup = app.MapGroup("/api/users").WithTags("Users");
        CreateProfile.MapEndpoint(usersGroup);
    }
}