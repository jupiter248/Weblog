using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblog.Application.Dtos.ContributorDtos;
using Weblog.Application.Dtos.MediaDtos;
using Weblog.Application.Dtos.TagDtos;

namespace Weblog.Application.Dtos.ArticleDtos
{
    public class ArticleSummaryDto
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required bool IsPublished { get; set; }
        public required int Viewers { get; set; }
        public required int Likes { get; set; } 
        public DateTimeOffset? PublishedAt { get; set; }
        public List<MediumDto>? MediumDtos { get; set; }
        public List<ContributorDto>? ContributorDtos { get; set; }

    }
}