using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Weblog.Application.Dtos.MediaDtos;

namespace Weblog.Application.Validations.Medium
{
    public class UpdateMediumValidator : AbstractValidator<UpdateMediumDto>
    {
        public UpdateMediumValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("")
                .MaximumLength(80).WithMessage("");


            RuleFor(x => x.Path)
                .NotEmpty().WithMessage("")
                .MaximumLength(100).WithMessage("");


            RuleFor(x => x.AltText)
                .NotEmpty().WithMessage("")
                .MaximumLength(200).WithMessage("");
        }
    }
}