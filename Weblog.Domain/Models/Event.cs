using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Weblog.Domain.Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Slug { get; set; }
        public required string Context { get; set; }
        public int Viewers { get; set; } 
        public int? Capacity { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public bool IsDisplayed { get; set; } = false;
        public bool IsFinished { get; set; } = false;
        public DateTimeOffset? DisplayedAt { get; set; }
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
        public DateTimeOffset? UpdatedAt { get; set; }
        public DateTimeOffset? PerformDate { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public List<Medium> Media { get; set; } = new List<Medium>();
        public List<Tag> Tags { get; set; } = new List<Tag>();
    }
}