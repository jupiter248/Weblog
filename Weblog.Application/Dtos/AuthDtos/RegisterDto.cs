using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblog.Application.Dtos.AuthDtos
{
    public class RegisterDto
    {
        public required string Username { get; set; }
        public required string PhoneNumber { get; set; }
        public string? Email { get; set; }  
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Password { get; set; }
        public required string RepetitionPassword { get; set; }
    }
}