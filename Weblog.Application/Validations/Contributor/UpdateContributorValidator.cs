using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Weblog.Application.Dtos.ContributorDtos;

namespace Weblog.Application.Validations.Contributor
{
    public class UpdateContributorValidator : AbstractValidator<UpdateContributorDto>
    {
        public UpdateContributorValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("")
                .Length(2, 50).WithMessage("");

            RuleFor(x => x.FamilyName)
                .NotEmpty().WithMessage("")
                .Length(2, 50).WithMessage("");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("")
                .MaximumLength(800).WithMessage("");
        }
    }
}