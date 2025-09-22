using Microsoft.EntityFrameworkCore;
using Teams.API.Features.Projects.CreateProject;
using Teams.API.Features.Projects.GetProjectById;
using Teams.Infrastructure;

namespace Teams.API.Extensions;

internal static class Extensions
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {
        var services = builder.Services;

        builder.Services.AddDbContext<TeamDbContext>(options =>
        {
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
        });
        
        builder.Services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblyContaining<Program>();
        });
    }

    public static void MapEndpoints(this IEndpointRouteBuilder app)
    {
        CreateProjectEndpoint.Map(app);
        GetProjectByIdEndpoint.Map(app);
    }
}