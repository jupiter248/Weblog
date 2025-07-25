using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Weblog.Application.Dtos.PodcastDtos
{
    public class UpdatePodcastDto
    {
        [MaxLength(50 , ErrorMessage = "Name can not be more than 50 char")]
        public required string Name { get; set; }
        public int CategoryId { get; set; }
        [MinLength(25 , ErrorMessage = "Description can not be less than 25 char")]
        public required string Description { get; set; }
        public required string Link { get; set; }
        public required bool IsDisplayed { get; set; }
    }
}