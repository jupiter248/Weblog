using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Weblog.Application.Dtos.MediaDtos;

namespace Weblog.Application.Validations.Medium
{
    public class UploadMediumValidator : AbstractValidator<UploadMediumDto>
    {
        public UploadMediumValidator()
        {
            RuleFor(x => x.UploadedFile)
                .NotNull().WithMessage("")
                .Must(f => f.Length > 0).WithMessage("")
                .Must(f => f.Length <= 5 * 1024 * 1024).WithMessage("");

            RuleFor(x => x.AltText)
                .NotEmpty().WithMessage("")
                .MaximumLength(200).WithMessage("");

            RuleFor(x => x.EntityType)
                .IsInEnum().WithMessage("");

            RuleFor(x => x.MediumType)
                .IsInEnum().WithMessage("");

        }
    }
}