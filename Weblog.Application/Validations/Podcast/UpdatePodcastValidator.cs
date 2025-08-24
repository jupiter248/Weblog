using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Weblog.Application.Dtos.PodcastDtos;
using Weblog.Domain.Errors.Podcast;

namespace Weblog.Application.Validations.Podcast
{
    public class UpdatePodcastValidator : AbstractValidator<UpdatePodcastDto>
    {
        public UpdatePodcastValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(PodcastErrorCodes.PodcastNameRequired)
                .MaximumLength(100).WithMessage(PodcastErrorCodes.PodcastNameMaxLength);

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage(PodcastErrorCodes.PodcastDescriptionRequired)
                .MaximumLength(800).WithMessage(PodcastErrorCodes.PodcastDescriptionMaxLength);

            RuleFor(x => x.Link)
                .NotEmpty().WithMessage(PodcastErrorCodes.PodcastLinkRequired)
                .MaximumLength(300).WithMessage(PodcastErrorCodes.PodcastLinkMaxLength)
                .Must(link => Uri.IsWellFormedUriString(link, UriKind.Absolute)).WithMessage(PodcastErrorCodes.PodcastLinkInvalid);
        }
    }
}