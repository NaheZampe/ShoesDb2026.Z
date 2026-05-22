using FluentValidation;
using ShoesDb2026.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoesDb2026.Services.Validators
{
    public class BrandValidator:AbstractValidator<Brand>
    {
        public BrandValidator()
        {
            RuleFor(b => b.Name)
                .NotEmpty().WithMessage("The brand name is required.")
                .MaximumLength(100).WithMessage("The brand name must not exceed 100 characters.");
        }
    }
}
