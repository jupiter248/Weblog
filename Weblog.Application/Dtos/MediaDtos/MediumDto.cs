using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblog.Domain.Enums;

namespace Weblog.Application.Dtos.MediaDtos
{
    public class MediumDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Path { get; set; }
        public bool IsPrimary { get; set; } = false;
        public MediumType MediumType { get; set; }
        public MediumParentType MediumParentType { get; set; }
        public int? ArticleId { get; set; }
        public int? EventId { get; set; }
        public int? PodcastId { get; set; }
        public int? ContributorId { get; set; }

    }
}