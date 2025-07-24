using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblog.Application.Dtos.UserDtos
{
    public class UpdateUserDto
    {
        public required string Username { get; set; }
        public required string PhoneNumber { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string OldPassword { get; set; }
        public required string NewPassword { get; set; }


    }
}