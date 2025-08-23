using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Weblog.Application.Dtos.TagDtos;

namespace Weblog.Application.Validations.Tag
{
    public class AddTagValidator : AbstractValidator<AddTagDto>
    {
        public AddTagValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("")
                .MaximumLength(80).WithMessage("");
        }
    }
}