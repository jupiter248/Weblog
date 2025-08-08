using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Weblog.Application.Dtos.MediaDtos
{
    public class UpdateMediumDto
    {
        public required string Name { get; set; }
        [Url]
        public required string Path { get; set; }
        public required string AltText { get; set; }
        public bool IsPrimary { get; set; }
        public bool IsOnPoster { get; set; }

    }
}