using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Weblog.Application.Dtos.ContributorDtos;
using Weblog.Domain.Errors.Contributor;

namespace Weblog.Application.Validations.Contributor
{
    public class UpdateContributorValidator : AbstractValidator<UpdateContributorDto>
    {
        public UpdateContributorValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage(ContributorErrorCodes.ContributorFirstNameRequired)
                .Length(2, 100).WithMessage(ContributorErrorCodes.ContributorFirstNameLength);

            RuleFor(x => x.FamilyName)
                .NotEmpty().WithMessage(ContributorErrorCodes.ContributorLastNameRequired)
                .Length(2, 100).WithMessage(ContributorErrorCodes.ContributorLastNameLength);

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage(ContributorErrorCodes.ContributorDescriptionRequired)
                .MaximumLength(800).WithMessage(ContributorErrorCodes.ContributorDescriptionMaxLength);
        }
    }
}