using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblog.Application.Dtos.ContributorDtos
{
    public class ContributorDto
    {
        public int Id { get; set; }
        public required string FirstName{ get; set; } 
        public required string FamilyName{ get; set; } 

    }
}