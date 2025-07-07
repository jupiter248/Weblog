using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Weblog.Domain.Models;

namespace Weblog.Domain.Models
{
    public class AppUser : IdentityUser 
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string FullName { get; set; }
        public List<Comment> Comments { get; set; } = new List<Comment>();
        public List<Medium> Media { get; set; } = new List<Medium>();
    }
}