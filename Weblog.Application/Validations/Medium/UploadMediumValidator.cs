using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Weblog.Application.Dtos.MediaDtos;
using Weblog.Domain.Errors.Medium;

namespace Weblog.Application.Validations.Medium
{
    public class UploadMediumValidator : AbstractValidator<UploadMediumDto>
    {
        public UploadMediumValidator()
        {
            RuleFor(x => x.UploadedFile)
                .NotNull().WithMessage(MediumErrorCodes.MediumFileRequired)
                .Must(f => f.Length <= 5 * 1024 * 1024).WithMessage(MediumErrorCodes.MediumFileSizes);

            RuleFor(x => x.AltText)
                .NotEmpty().WithMessage(MediumErrorCodes.MediumAltTextRequired)
                .MaximumLength(200).WithMessage(MediumErrorCodes.MediumAltTextMaxLength);

            RuleFor(x => x.EntityType)
                .IsInEnum().WithMessage(MediumErrorCodes.MediumEntityTypeInvalid);

            RuleFor(x => x.MediumType)
                .IsInEnum().WithMessage(MediumErrorCodes.MediumTypeInvalid);
        }
    }
}