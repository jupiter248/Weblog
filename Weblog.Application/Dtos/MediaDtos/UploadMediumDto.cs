using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Weblog.Domain.Enums;

namespace Weblog.Application.Dtos.MediaDtos
{
    public class UploadMediumDto
    {
        public required IFormFile UploadedFile { get; set; }
        public bool IsPrimary { get; set; }
        public EntityType EntityType { get; set; }
        public MediumType MediumType { get; set; }
        public int ParentTypeId { get; set; }
        public string? AppUserId { get; set; }
        public bool IsOnPoster { get; set; }

        // public int? ArticleId { get; set; }
        // public int? EventId { get; set; }
        // public int? PodcastId  { get; set; }
        // public int? ContributorId { get; set; }

    }
}