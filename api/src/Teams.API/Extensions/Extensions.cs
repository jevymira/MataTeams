using System.Text;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Teams.API.Features.Projects;
using Teams.API.Features.Projects.CreateProject;
using Teams.API.Logging;
using Teams.API.Validation;
using Teams.Domain.Aggregates.MemberAggregate;
using Teams.Domain.Aggregates.ProjectAggregate;
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
                var member = context.Set<Member>()
                    .FirstOrDefault(m => m.IdentityGuid == builder.Configuration["SeedUser:IdentityGuid"]);
                if (member == null)
                {
                    member = new Member(builder.Configuration["SeedUser:IdentityGuid"]!);
                    context.Set<Member>().Add(member);
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
                        member.Id);
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
    }

    public static void MapEndpoints(this IEndpointRouteBuilder app)
    {
        var projectsMapGroup = app.MapGroup("/api/projects")
            .WithTags("Projects");
        
        GetProjectById.MapEndpoint(projectsMapGroup); 
        CreateProjectEndpoint.Map(projectsMapGroup);
    }
}