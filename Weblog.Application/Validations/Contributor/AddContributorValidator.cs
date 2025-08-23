using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Weblog.Application.Dtos.ContributorDtos;

namespace Weblog.Application.Validations.Contributor
{
    public class AddContributorValidator : AbstractValidator<AddContributorDto>
    {
        public AddContributorValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("")
                .Length(2, 100).WithMessage("");

            RuleFor(x => x.FamilyName)
                .NotEmpty().WithMessage("")
                .Length(2, 100).WithMessage("");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("")
                .MaximumLength(800).WithMessage("");
        }
    }
}