using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Teams.Infrastructure.Messaging;

namespace Teams.Infrastructure.Messaging;

public static class MassTransitSetup
{
    public static IServiceCollection AddTeamsMessaging(this IServiceCollection services)
    {
        services.AddMassTransit(x =>
        {
            x.AddConsumer<ProjectCreatedConsumer>();

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host("localhost", "/", h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });
                cfg.ConfigureEndpoints(context);
            });
        });
        return services;
    }
}