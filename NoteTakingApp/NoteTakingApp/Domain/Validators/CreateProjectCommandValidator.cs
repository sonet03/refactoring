using FluentValidation;
using NoteTakingApp.Application.Commands;

namespace NoteTakingApp.Domain.Validators;

public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
{
    public CreateProjectCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Project Name is required.")
            .MinimumLength(3).WithMessage("Project Name must be at least 3 characters long.")
            .MaximumLength(100).WithMessage("Project Name must not exceed 100 characters.");
    }
}