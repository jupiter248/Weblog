using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Weblog.Application.Dtos.EventDtos
{
    public class UpdateEventDto
    {
        [MaxLength(50 , ErrorMessage = "Title can not be more than 50 char")]
        public required string Title { get; set; }
        [MinLength(50 , ErrorMessage = "Context can not be less than 50 char")]
        public required string Context { get; set; }
        [MinLength(25 , ErrorMessage = "Description can not be less than 25 char")]
        public required string Description { get; set; }
        public required string Place {get;set;}
        public required int CategoryId { get; set; }
        public int Capacity { get; set; }
        public bool IsDisplayed { get; set; }
        public bool IsFinished { get; set; }
        public DateTimeOffset? StartedAt { get; set; }
        public DateTimeOffset? FinishedAt { get; set; }
    }
}