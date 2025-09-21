using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Weblog.Application.Dtos.UserDtos;
using Weblog.Domain.Errors.User;

namespace Weblog.Application.Validations.User
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserDto>
    {
        public UpdateUserValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage(UserErrorCodes.UsernameRequired)
                .MaximumLength(40).WithMessage(UserErrorCodes.UsernameMaxLength);

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage(UserErrorCodes.PhoneRequired)
                .Matches(@"^\+?\d{10,15}$").WithMessage(UserErrorCodes.InvalidPhone);

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage(UserErrorCodes.UserFirstNameRequired)
                .Length(2, 50).WithMessage(UserErrorCodes.UserFirstNameMaxLength);

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage(UserErrorCodes.UserLastNameRequired)
                .Length(2, 100).WithMessage(UserErrorCodes.UserLastNameMaxLength);


        }
    }
}