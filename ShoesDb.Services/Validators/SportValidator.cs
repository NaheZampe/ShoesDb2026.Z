using FluentValidation;
using ShoesDb2026.Entities;

namespace ShoesDb2026.Services.Validators
{
    public class SportValidator : AbstractValidator<Sport>
    {
        public SportValidator()
        {
            RuleFor(s => s.SportName)
                .NotEmpty().WithMessage("Sport name is required.")
                .MaximumLength(100).WithMessage("Sport name must not exceed 100 characters.");
        }
    }
}
