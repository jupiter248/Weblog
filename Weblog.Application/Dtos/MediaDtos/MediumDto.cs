using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblog.Domain.Enums;

namespace Weblog.Application.Dtos.MediaDtos
{
    public class MediumDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Path { get; set; }
        public required string AltText { get; set; }
        public bool IsPrimary { get; set; } = false;
        public MediumType MediumType { get; set; }
        public EntityType EntityType { get; set; }
        public int ParentTypeId { get; set; }
        public bool IsOnPoster { get; set; } 
        public required string UserId { get; set; } 




    }
}