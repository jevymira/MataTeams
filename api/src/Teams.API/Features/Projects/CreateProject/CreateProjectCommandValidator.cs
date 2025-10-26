using FluentValidation;
using Teams.Domain.SharedKernel;

namespace Teams.API.Features.Projects.CreateProject;

public sealed class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
{
    public CreateProjectCommandValidator()
    {
        RuleFor(command => command.Name).NotEmpty();
        RuleFor(command => command.Description).NotEmpty();
        RuleFor(command => command.Type).NotEmpty();
        RuleFor(command => command.Status).NotEmpty();
        
        RuleForEach(command => command.Roles).ChildRules(role =>
        {
            role.RuleFor(r => r.PositionCount).GreaterThan(0);
        });
    }
}