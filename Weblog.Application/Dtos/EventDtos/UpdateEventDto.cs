using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblog.Application.Dtos.EventDtos
{
    public class UpdateEventDto
    {
        public required string Title { get; set; }
        public required string Context { get; set; }
        public required int CategoryId { get; set; }
        public int Capacity { get; set; }
        public bool IsDisplayed { get; set; }
        public bool IsFinished { get; set; }
    }
}