using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblog.Application.Dtos.MediaDtos;
using Weblog.Domain.Enums;

namespace Weblog.Application.Dtos.ContributorDtos
{
    public class ContributorDto
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string FamilyName { get; set; }
        public required string FullName { get; set; }
        public required string Description { get; set; }
        public List<MediumDto>? MediumDtos { get; set; }
        public DateTimeOffset CreatedOn { get; set; }

        
    }
}