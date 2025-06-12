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
        public required string Slug { get; set; }
        public required string Context { get; set; }
        public int Viewers { get; set; } = 1;
        public int? Capacity { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public bool IsDisplayed { get; set; } = false;
        public DateTime? DisplayedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? PerformDate { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public List<Medium> Media { get; set; } = new List<Medium>();
        public List<Tag> Tags { get; set; } = new List<Tag>();


    }
}