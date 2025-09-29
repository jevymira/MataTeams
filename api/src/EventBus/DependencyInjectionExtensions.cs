using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace EventBus;

public static class DependencyInjectionExtensions
{
    private static readonly string hostname = "rabbitmq://localhost";
    private static readonly string username = "guest"; // RabbitMQ default
    private static readonly string password = "guest"; // RabbitMQ default
    
    public static IServiceCollection AddProducerRabbitMq(this IServiceCollection services)
    {
        services.AddMassTransit(options =>
        {
            options.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(new Uri(hostname), h =>
                {
                    h.Username(username);
                    h.Password(password);
                });
            });
        });
        
        return services;
    }
}