using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Weblog.Application.Dtos.FavoriteListDtos;
using Weblog.Domain.Errors.Favorite;

namespace Weblog.Application.Validations.FavoriteList
{
    public class AddFavoriteListValidator : AbstractValidator<AddFavoriteListDto>
    {
        public AddFavoriteListValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(FavoriteErrorCodes.FavoriteListNameRequired)
                .Length(2, 100).WithMessage(FavoriteErrorCodes.FavoriteListNameMaxLength);

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage(FavoriteErrorCodes.FavoriteListDescriptionRequired)
                .MaximumLength(400).WithMessage(FavoriteErrorCodes.FavoriteListDescriptionMaxLength);
        }
    }
}