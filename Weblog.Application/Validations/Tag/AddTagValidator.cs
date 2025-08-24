using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Weblog.Application.Dtos.TagDtos;
using Weblog.Domain.Errors.Tag;

namespace Weblog.Application.Validations.Tag
{
    public class AddTagValidator : AbstractValidator<AddTagDto>
    {
        public AddTagValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage(TagErrorCodes.TagTitleRequired)
                .MaximumLength(80).WithMessage(TagErrorCodes.TagTitleMaxLength);
        }
    }
}