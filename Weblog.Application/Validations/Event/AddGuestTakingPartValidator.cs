using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Weblog.Application.Dtos.EventDtos;
using Weblog.Domain.Errors.User;

namespace Weblog.Application.Validations.Event
{
    public class AddGuestTakingPartValidator : AbstractValidator<AddGuestTakinPartDto>
    {
        public AddGuestTakingPartValidator()
        {
            RuleFor(x => x.GuestPhone)
                .NotEmpty().WithMessage(UserErrorCodes.PhoneRequired)
                .Matches(@"^\+?\d{10,15}$").WithMessage(UserErrorCodes.InvalidPhone);

            RuleFor(x => x.GuestName)
                .NotEmpty().WithMessage(UserErrorCodes.UserFirstNameRequired)
                .Length(2, 50).WithMessage(UserErrorCodes.UserFirstNameMaxLength);

            RuleFor(x => x.GuestFamily)
                .NotEmpty().WithMessage(UserErrorCodes.UserLastNameRequired)
                .Length(2, 100).WithMessage(UserErrorCodes.UserLastNameMaxLength);
            
        }
    }
}