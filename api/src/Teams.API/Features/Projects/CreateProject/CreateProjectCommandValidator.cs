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
            role.RuleForEach(r => r.Skills).ChildRules(skill =>
            {
                skill.RuleFor(s => s.Proficiency)
                    .Must(BeAValidProficiency)
                    .WithMessage(s => $"Proficiency '{s.Proficiency}' is invalid.");
            });
        });
    }

    private bool BeAValidProficiency(string proficiency)
    {
        return Enum.TryParse<Proficiency>(proficiency, true, out _);
    }
}