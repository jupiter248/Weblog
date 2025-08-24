using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Weblog.Application.Dtos.CategoryDtos;
using Weblog.Domain.Errors.Category;

namespace Weblog.Application.Validations.Category
{
    public class UpdateCategoryValidator : AbstractValidator<UpdateCategoryDto>
    {
        public UpdateCategoryValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(CategoryErrorCodes.CategoryNameRequired)
                .MaximumLength(100).WithMessage(CategoryErrorCodes.CategoryNameMaxLength);

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage(CategoryErrorCodes.CategoryDescriptionRequired)
                .MaximumLength(500).WithMessage(CategoryErrorCodes.CategoryDescriptionMaxLength);

            RuleFor(x => x.EntityType)
                .NotNull().WithMessage(CategoryErrorCodes.CategoryEntityTypeRequired)
                .IsInEnum().WithMessage(CategoryErrorCodes.CategoryInvalidEntityType);   
        }
    }
}