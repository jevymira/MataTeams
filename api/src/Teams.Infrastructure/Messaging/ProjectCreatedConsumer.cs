using MassTransit;
using Teams.Contracts;

namespace Teams.Infrastructure.Messaging;

public class ProjectCreatedConsumer : IConsumer<ProjectCreated>
{
    public Task Consume(ConsumeContext<ProjectCreated> context)
    {
        var message = context.Message;
        Console.WriteLine($"[Recommendation] New project detected: {message.Name} ({message.ProjectId})");

        // Recommendation logic goes here
        return Task.CompletedTask;
    }
}
