using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Weblog.Domain.Enums;

namespace Weblog.Application.Dtos.MediaDtos
{
    public class UploadMediumDto
    {
        public required IFormFile UploadedFile { get; set; }
        public required string AltText { get; set; }
        public MediumType MediumType { get; set; }
        public EntityType EntityType { get; set; }
        public int EntityId { get; set; }
        public bool IsPrimary { get; set; }
        public bool IsOnPoster { get; set; }
    }
}