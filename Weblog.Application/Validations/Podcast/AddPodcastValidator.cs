using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Weblog.Application.Dtos.PodcastDtos;

namespace Weblog.Application.Validations.Podcast
{
    public class AddPodcastValidator : AbstractValidator<AddPodcastDto>
    {
        public AddPodcastValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("")
                .MaximumLength(100).WithMessage("characters");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("")
                .MaximumLength(800).WithMessage("");

            RuleFor(x => x.Link)
                .NotEmpty().WithMessage("")
                .MaximumLength(300).WithMessage("")
                .Must(link => Uri.IsWellFormedUriString(link, UriKind.Absolute))
                .WithMessage("");

            RuleFor(x => x.IsDisplayed)
                .NotEmpty().WithMessage("");
        }
    }
}