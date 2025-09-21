using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Weblog.Application.Dtos.UserDtos;
using Weblog.Domain.Errors.User;

namespace Weblog.Application.Validations.User
{
    public class ChangeUserPasswordValidator : AbstractValidator<UpdateUserPasswordDto>
    {
        public ChangeUserPasswordValidator()
        {
            RuleFor(x => x.OldPassword)
                .NotEmpty().WithMessage(UserErrorCodes.PasswordRequired)
                .Length(8, 100).WithMessage(UserErrorCodes.PasswordLength);

            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage(UserErrorCodes.PasswordRequired)
                .Length(8, 100).WithMessage(UserErrorCodes.PasswordLength);
        }
    }
}