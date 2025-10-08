using System.Text;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Teams.API.Logging;
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
            options.UseSeeding((context, _) =>
            {
                /*
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
                    .FirstOrDefault(m => m.IdentityGuid == builder.Configuration["SeedUser:IdentityGuid"]);
                if (user == null)
                {
                    user = new User(builder.Configuration["SeedUser:IdentityGuid"]!);
                    context.Set<User>().Add(user);
                    context.SaveChanges();
                }
                
                var userSkill = context.Set<UserSkill>()
                    .FirstOrDefault(s => s.UserId == user.Id);
                if (userSkill == null)
                {
                    userSkill = new UserSkill(user.Id, js.Id, Proficiency.Interested);
                    context.Set<UserSkill>().Add(userSkill);
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
                    // Add `Frontend` Role with `JavaScript` and `React Skills.
                    project.AddProjectRole(1, 2);
                    project.Roles.First().AddProjectSkill(2, Proficiency.Beginner);
                    project.Roles.First().AddProjectSkill(3, Proficiency.Interested);
                    // Add `Backend` Role with `Java` Skill.
                    project.AddProjectRole(2, 2);
                    project.Roles.Last().AddProjectSkill(1, Proficiency.Intermediate);
                    context.Set<Project>().Add(project);
                    context.SaveChanges();
                } 
                */
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
    }

    public static void MapEndpoints(this IEndpointRouteBuilder app)
    {
        var projectsMapGroup = app.MapGroup("/api/projects")
            .WithTags("Projects");
        
        /*
        GetProjectById.MapEndpoint(projectsMapGroup); 
        CreateProjectEndpoint.Map(projectsMapGroup);
        */
    }
}