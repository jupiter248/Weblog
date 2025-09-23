using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblog.Application.Dtos.CommentDtos;
using Weblog.Application.Dtos.ContributorDtos;
using Weblog.Application.Dtos.MediaDtos;
using Weblog.Application.Dtos.TagDtos;

namespace Weblog.Application.Dtos.ArticleDtos
{
    public class ArticleDto
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public string? AboveTitle { get; set; }
        public string? BelowTitle { get; set; }
        public required string Slug { get; set; }
        public required string Context { get; set; }
        public required string Description { get; set; }
        public required int CategoryId { get; set; }
        public required string CategoryName { get; set; }
        public required bool IsPublished { get; set; }
        public required int ViewCount { get; set; }
        public required int LikeCount { get; set; }
        public required string CreatedAt { get; set; }
        public string? UpdatedAt { get; set; }
        public string? PublishedAt { get; set; }
        public List<MediumDto>? MediumDtos { get; set; }
        public List<TagDto>? TagDtos { get; set; }
        public List<ContributorDto>? ContributorDtos { get; set; }
    }
}