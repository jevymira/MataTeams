using FluentValidation;
using Teams.Domain.SharedKernel;

namespace Teams.API.Features.Projects.CreateProject;

public sealed class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
{
    public CreateProjectCommandValidator()
    {
        RuleFor(command => command.Name).NotEmpty();
        RuleFor(command => command.Description).NotEmpty();
        RuleFor(command => command.Type)
            .Must(value => ProjectType.All.Any(status
                => status.Name == value))
            .WithMessage("Invalid Project Type");
        RuleFor(command => command.Status)
            .Must(value => Enum.TryParse<ProjectStatus>(value, true, out _))
            .WithMessage("Invalid Project Status");
    }
}