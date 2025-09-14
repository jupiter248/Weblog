using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Weblog.Application.Dtos.UserProfileDtos
{
    public class UserProfileDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Path { get; set; }
        public required string UserId { get; set; }
        public required string UserName { get; set; }       
    }
}