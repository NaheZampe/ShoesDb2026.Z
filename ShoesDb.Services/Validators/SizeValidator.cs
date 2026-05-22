using FluentValidation;
using ShoesDb2026.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoesDb2026.Services.Validators
{
    public class SizeValidator:AbstractValidator<SiZe>
    {
        public SizeValidator()
        {
            RuleFor(s => s.SizeNumber)
                .GreaterThan(0).WithMessage("The size number must be greater than 0.");
        }
    }
}
