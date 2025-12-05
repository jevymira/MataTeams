using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Teams.Infrastructure.Messaging;
using Teams.Contracts;

namespace Teams.Infrastructure.Messaging;

public static class MassTransitSetup
{
    public static IServiceCollection AddTeamsMessaging(this IServiceCollection services)
    {
        services.AddMassTransit(x =>
        {

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host("localhost", "/", h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });
                
                cfg.Publish<ProjectCreated>(p => 
                {
                    p.Durable = true;
                    p.AutoDelete = false;
                    p.ExchangeType = "fanout"; 
                });
                
                cfg.Publish<UserProfileFetched>(p => 
                {
                    p.Durable = true;
                    p.AutoDelete = false;
                    p.ExchangeType = "fanout";
                });
                
                cfg.Publish<UserCreated>(p => 
                {
                    p.Durable = true;
                    p.AutoDelete = false;
                    p.ExchangeType = "fanout";
                });

                cfg.ConfigureEndpoints(context);
            });
        });
        

        services.AddOptions<MassTransitHostOptions>()
            .Configure(options =>
            {
                options.WaitUntilStarted = true;
                options.StartTimeout = TimeSpan.FromSeconds(30);
            });
        
        return services;
    }
}