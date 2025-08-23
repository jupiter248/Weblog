using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Weblog.Application.Dtos.UserDtos;

namespace Weblog.Application.Validations.User
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserDto>
    {
        public UpdateUserValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("")
                .MaximumLength(50).WithMessage("");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("")
                .Matches(@"^\+?\d{10,15}$").WithMessage("");

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("")
                .Length(2, 100).WithMessage("");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("")
                .Length(2, 100).WithMessage("");

            RuleFor(x => x.OldPassword)
                .NotEmpty().WithMessage("")
                .Length(8, 60).WithMessage("");

            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("")
                .Length(8, 60).WithMessage("");
        }
    }
}