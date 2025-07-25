using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Weblog.Domain.JoinModels;

namespace Weblog.Domain.Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Slug { get; set; }
        public required string Context { get; set; }
        public required string Description { get; set; }
        public required string Place { get; set; }
        public int? Capacity { get; set; }
        public int CategoryId { get; set; }
        public required Category Category { get; set; }
        public bool IsDisplayed { get; set; } = false;
        public bool IsFinished { get; set; } = false;
        public DateTimeOffset? DisplayedAt { get; set; }
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
        public DateTimeOffset? UpdatedAt { get; set; }
        public DateTimeOffset? StartedAt { get; set; }
        public DateTimeOffset? FinishedAt { get; set; }
        // public TimeSpan? StartTime { get; set; }
        // public TimeSpan? EndTime { get; set; }
        public List<Medium> Media { get; set; } = new List<Medium>();
        public List<Tag> Tags { get; set; } = new List<Tag>();
        public List<Contributor> Contributors { get; set; } = new List<Contributor>();
        public List<Comment> Comments { get; set; } = new List<Comment>();
        public List<FavoriteEvent> FavoriteEvents { get; set; } = new List<FavoriteEvent>();
        public List<TakingPart> TakingParts { get; set; } = new List<TakingPart>();  
        public List<LikeContent> LikeContents { get; set; } = new List<LikeContent>();
        public List<ViewContent> ViewContents { get; set; } = new List<ViewContent>();
    }
}