using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblog.Application.Dtos.MediaDtos;

namespace Weblog.Application.Dtos.AuthDtos
{
    public class AuthResponseDto
    {
        public required string Username { get; set; }
        public required string PhoneNumber { get; set; }
        public string? Email { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string FullName { get; set; }
        public List<MediumDto>? MediumDtos { get; set; }
    }
}