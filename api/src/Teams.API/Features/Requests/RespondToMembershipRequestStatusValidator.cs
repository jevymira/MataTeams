using FluentValidation;
using Teams.Domain.Aggregates.ProjectAggregate;

namespace Teams.API.Features.Requests;

public sealed class RespondToMembershipRequestCommandValidator : AbstractValidator<RespondToMembershipRequestCommand>
{
    public RespondToMembershipRequestCommandValidator()
    {
        RuleFor(command => command.NewStatus)
            .Must(BeAValidStatus)
            .WithMessage(s => $"Status '{s.NewStatus}' is invalid.");
    }

    private bool BeAValidStatus(string status)
    {
        return Enum.TryParse<TeamMembershipRequestStatus>(status, false, out _);
    }
}