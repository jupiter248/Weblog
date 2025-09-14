using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
        public required IList<string> Roles { get; set; }
        public required string UserActiveDuration { get; set; }
        public required string CreatedAt { get; set; }
        public string? UpdatedAt { get; set; }
        public List<Profile>? Profiles { get; set; }
    }
}