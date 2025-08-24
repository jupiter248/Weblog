using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Weblog.Application.Dtos.EventDtos;
using Weblog.Domain.Errors.Event;

namespace Weblog.Application.Validations.Event
{
    public class AddEventValidator : AbstractValidator<AddEventDto>
    {
        public AddEventValidator()
        {
            RuleFor(t => t.Title)
                .NotEmpty().WithMessage(EventErrorCodes.EventNotFound)
                .MaximumLength(150).WithMessage(EventErrorCodes.EventTitleMaxLength);

            RuleFor(d => d.Description)
                .Empty().WithMessage(EventErrorCodes.EventDescriptionRequired)
                .MaximumLength(600).WithMessage(EventErrorCodes.EventDescriptionMaxLength);

            RuleFor(c => c.Context)
                .Empty().WithMessage(EventErrorCodes.EventContextRequired);

            RuleFor(p => p.Place)
                .Empty().WithMessage(EventErrorCodes.EventPlaceRequired)
                .MaximumLength(100).WithMessage(EventErrorCodes.EventPlaceMaxLength);
        }
    }
}