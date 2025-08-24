using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Weblog.Application.Dtos.MediaDtos;
using Weblog.Domain.Errors.Medium;

namespace Weblog.Application.Validations.Medium
{
    public class UpdateMediumValidator : AbstractValidator<UpdateMediumDto>
    {
        public UpdateMediumValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(MediumErrorCodes.MediumNameRequired)
                .MaximumLength(80).WithMessage(MediumErrorCodes.MediumNameMaxLength);


            RuleFor(x => x.Path)
                .NotEmpty().WithMessage(MediumErrorCodes.MediumPathRequired)
                .MaximumLength(100).WithMessage(MediumErrorCodes.MediumPathMaxLength);


            RuleFor(x => x.AltText)
                .NotEmpty().WithMessage(MediumErrorCodes.MediumAltTextRequired)
                .MaximumLength(200).WithMessage(MediumErrorCodes.MediumAltTextMaxLength);
        }
    }
}