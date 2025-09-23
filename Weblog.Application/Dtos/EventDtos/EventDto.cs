using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblog.Application.Dtos.CommentDtos;
using Weblog.Application.Dtos.ContributorDtos;
using Weblog.Application.Dtos.MediaDtos;
using Weblog.Application.Dtos.TagDtos;

namespace Weblog.Application.Dtos.EventDtos
{
    public class EventDto
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public string? AboveTitle { get; set; }
        public string? BelowTitle { get; set; }
        public required string Slug { get; set; }
        public required string Context { get; set; }
        public required string Description { get; set; }
        public int Capacity { get; set; }
        public required string Place { get; set; }
        public int CategoryId { get; set; }
        public required string CategoryName { get; set; }
        public bool IsDisplayed { get; set; }
        public bool IsFinished { get; set; }
        public required int ViewCount { get; set; }
        public required int LikeCount { get; set; }
        public required string CreatedAt { get; set; }
        public string? UpdatedAt { get; set; }
        public string? DisplayedAt { get; set; }
        public string? StartedAt { get; set; }
        public string? FinishedAt { get; set; }
        public List<MediumDto>? MediumDtos { get; set; }
        public List<TagDto>? TagDtos { get; set; }
        public List<ContributorDto>? ContributorDtos { get; set; }
    }
}