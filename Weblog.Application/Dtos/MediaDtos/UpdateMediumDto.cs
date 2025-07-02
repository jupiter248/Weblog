using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblog.Application.Dtos.MediaDtos
{
    public class UpdateMediumDto
    {
        public required string Name { get; set; }
        public required string Path { get; set; }
        public bool IsPrimary { get; set; }
        public bool IsOnPoster { get; set; }

    }
}