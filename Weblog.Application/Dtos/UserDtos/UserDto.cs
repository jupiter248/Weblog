using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblog.Application.Dtos.MediaDtos;

namespace Weblog.Application.Dtos.UserDtos
{
    public class UserDto
    {
        public required string Id { get; set; }
        public required string Username { get; set; }
        public required string PhoneNumber { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string FullName { get; set; }
        public required DateTimeOffset CreatedAt { get; set; }
        public required DateTimeOffset UpdatedAt { get; set; }
        public List<MediumDto>? Media { get; set; }
    }
}