using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblog.Application.Dtos.ContributorDtos;
using Weblog.Application.Dtos.MediaDtos;

namespace Weblog.Application.Dtos.PodcastDtos
{
    public class PodcastSummaryDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required bool IsDisplayed { get; set; }
        public List<MediumDto>? MediumDtos { get; set; }
        public List<ContributorDto>? ContributorDtos { get; set; }

    }
}