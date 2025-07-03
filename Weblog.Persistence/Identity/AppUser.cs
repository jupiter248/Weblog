using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Weblog.Domain.Models;

namespace Weblog.Infrastructure.Identity
{
    public class AppUser : IdentityUser
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string FullName { get; set; }
        public int MediumId { get; set; }
        public required Medium Medium { get; set; }

    }
}