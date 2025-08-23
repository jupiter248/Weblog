using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Weblog.Application.Dtos.FavoriteListDtos;

namespace Weblog.Application.Validations.FavoriteList
{
    public class AddFavoriteListValidator : AbstractValidator<AddFavoriteListDto>
    {
        public AddFavoriteListValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("")
                .Length(2, 100).WithMessage("");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("")
                .MaximumLength(400).WithMessage("");
        }
    }
}