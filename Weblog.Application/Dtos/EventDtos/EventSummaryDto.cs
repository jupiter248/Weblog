using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblog.Application.Dtos.ContributorDtos;
using Weblog.Application.Dtos.MediaDtos;

namespace Weblog.Application.Dtos.EventDtos
{
    public class EventSummaryDto
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required string Place { get; set; }
        public bool IsDisplayed { get; set; }
        public bool IsFinished { get; set; }
        public required int ViewCount { get; set; }
        public required int LikeCount { get; set; }
        public List<ContributorDto>? ContributorDtos { get; set; }
        public List<MediumDto>? MediumDtos { get; set; }

    }
}