using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Weblog.Domain.Enums;

namespace Weblog.Domain.Models
{
    public class UserProfile
    {
        [Key]
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Path { get; set; }
        public required string UserId { get; set; }        
        public AppUser?  AppUser { get; set; }        
    }
}