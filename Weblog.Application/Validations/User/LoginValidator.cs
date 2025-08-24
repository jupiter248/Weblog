using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Weblog.Application.Dtos.AuthDtos;
using Weblog.Domain.Errors.User;

namespace Weblog.Application.Validations.User
{
    public class LoginValidator : AbstractValidator<LoginDto>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage(UserErrorCodes.UsernameRequired)
                .MaximumLength(40).WithMessage(UserErrorCodes.UsernameMaxLength);
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage(UserErrorCodes.PasswordRequired)
                .Length(8, 100).WithMessage(UserErrorCodes.PasswordLength);
        }
    }
}