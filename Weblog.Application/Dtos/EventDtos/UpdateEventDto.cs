using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Weblog.Application.Dtos.EventDtos
{
    public class UpdateEventDto
    {
        public required string Title { get; set; }
        public string? AboveTitle { get; set; }
        public string? BelowTitle { get; set; } 
        public required string Context { get; set; }
        public required string Description { get; set; }
        public required string Place {get;set;}
        public required int CategoryId { get; set; }
        public int Capacity { get; set; }
        public bool IsDisplayed { get; set; }
        public bool IsFinished { get; set; }
        public DateTimeOffset StartedAt { get; set; }
        public DateTimeOffset FinishedAt { get; set; }
    }
}