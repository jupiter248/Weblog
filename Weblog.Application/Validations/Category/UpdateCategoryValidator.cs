using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Weblog.Application.Dtos.CategoryDtos;

namespace Weblog.Application.Validations.Category
{
    public class UpdateCategoryValidator : AbstractValidator<UpdateCategoryDto>
    {
        public UpdateCategoryValidator()
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