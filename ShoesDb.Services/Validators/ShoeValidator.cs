using FluentValidation;
using ShoesDb2026.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoesDb2026.Services.Validators
{
    public class ShoeValidator:AbstractValidator<SportShoe>
    {
        public ShoeValidator()
        {
            RuleFor(s => s.Model)
                .NotEmpty()
                .WithMessage("Model is required.")
                .MaximumLength(100)
                .WithMessage("Model cannot exceed 100 characters.");
            RuleFor(s => s.Price).NotEmpty().WithMessage("Price is required");
            RuleFor(s => s.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
        }
    }
}
