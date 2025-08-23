using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Weblog.Application.Dtos.EventDtos;

namespace Weblog.Application.Validations.Event
{
    public class AddEventValidator : AbstractValidator<AddEventDto>
    {
        public AddEventValidator()
        {
            RuleFor(t => t.Title)
                .NotEmpty().WithMessage("")
                .MaximumLength(150).WithMessage("");

            RuleFor(d => d.Description)
                .Empty().WithMessage("")
                .MaximumLength(600).WithMessage("");

            RuleFor(c => c.Context)
                .Empty().WithMessage("");

            RuleFor(p => p.Place)
                .Empty().WithMessage("")
                .MaximumLength(100).WithMessage("");
        }
    }
}