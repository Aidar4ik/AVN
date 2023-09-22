﻿using FluentValidation;
using Web.Areas.Catalog.Models;

namespace Web.Areas.Catalog.Validators
{
    public class FacultyViewModelValidator : AbstractValidator<FacultyViewModel>
    {
        public FacultyViewModelValidator()
        {
            RuleFor(p => p.FacultyName)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            //RuleFor(p => p.Tax).GreaterThanOrEqualTo(1).WithMessage("{PropertyName} must be greater than 1");
        }
    }
}
