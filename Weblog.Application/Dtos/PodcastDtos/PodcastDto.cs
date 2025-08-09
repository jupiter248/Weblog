using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblog.Application.Dtos.CommentDtos;
using Weblog.Application.Dtos.ContributorDtos;
using Weblog.Application.Dtos.MediaDtos;
using Weblog.Application.Dtos.TagDtos;

namespace Weblog.Application.Dtos.PodcastDtos
{
    public class PodcastDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Slug { get; set; }
        public int CategoryId { get; set; }
        public required string CategoryName { get; set; }
        public required string Description { get; set; }
        public required string Link { get; set; }
        public required bool IsDisplayed { get; set; }
        public required int ViewCount { get; set; }
        public required int LikeCount { get; set; }
        public required string CreatedAt { get; set; }
        public string? DisplayedAt { get; set; }
        public string? UpdatedAt { get; set; }
        public List<MediumDto>? MediumDtos { get; set; }
        public List<TagDto>? TagDtos { get; set; }
        public List<ContributorDto>? ContributorDtos { get; set; }


    }
}