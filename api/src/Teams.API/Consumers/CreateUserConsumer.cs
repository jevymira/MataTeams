using EventBus.Commands;
using MassTransit;
using Teams.Domain.Aggregates.MemberAggregate;
using Teams.Infrastructure;

namespace Teams.API.Consumers;

public class CreateUserConsumer(TeamDbContext dbContext) : IConsumer<CreateUser>
{
    public async Task Consume(ConsumeContext<CreateUser> context)
    {
        var user = new Member(context.Message.IdentityGuid);
        dbContext.Members.Add(user);
        await dbContext.SaveChangesAsync();
    }
}