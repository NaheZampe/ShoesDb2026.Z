using FluentValidation;
using ShoesDb2026.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoesDb2026.Services.Validators
{
    public class GenreValidator:AbstractValidator<Genre>
    {
        public GenreValidator()
        {
            RuleFor(g => g.GenreName)
                .NotEmpty().WithMessage("Genre name is required.")
                .MaximumLength(100).WithMessage("Genre name must not exceed 100 characters.");
        }
    }
}
