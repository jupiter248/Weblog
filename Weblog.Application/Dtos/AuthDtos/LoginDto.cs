using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblog.Application.Dtos.AuthDtos
{
    public class LoginDto
    {
        public required string PhoneNumber { get; set; }
        public required string Username { get; set; }
        public required string Code { get; set; }
    }
}