using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblog.Domain.Errors.Podcast
{
    public class PodcastErrorCodes
    {
        public const string PodcastNotFound = "PODCAST_NOT_FOUND";
        public const string PodcastNameRequired = "PODCAST_NAME_REQUIRED";
        public const string PodcastNameMaxLength = "PODCAST_NAME_MAX_LENGTH";
        public const string PodcastDescriptionRequired = "PODCAST_DESCRIPTION_REQUIRED";
        public const string PodcastDescriptionMaxLength = "PODCAST_DESCRIPTION_MAX_LENGTH";
        public const string PodcastLinkRequired = "PODCAST_LINK_REQUIRED";
        public const string PodcastLinkMaxLength = "PODCAST_LINK_MAX_LENGTH";
        public const string PodcastLinkInvalid = "PODCAST_LINK_INVALID";


    }
}