using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Weblog.Application.Dtos.CategoryDtos;

namespace Weblog.Application.Validations.Category
{
    public class AddCategoryValidator : AbstractValidator<AddCategoryDto>
    {
        public AddCategoryValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("")
                .MaximumLength(100).WithMessage("");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("")
                .MaximumLength(500).WithMessage("");

            RuleFor(x => x.EntityType)
                .IsInEnum().WithMessage("");
        }
    }
}