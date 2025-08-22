using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Weblog.Application.Dtos.PodcastDtos
{
    public class AddPodcastDto
    {
        public required string Name { get; set; }
        public int CategoryId { get; set; }
        public required string Description { get; set; }
        public required string Link { get; set; }
        public required bool IsDisplayed { get; set; }
        
    }
}