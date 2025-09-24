using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblog.Domain.Models;

namespace Weblog.Domain.JoinModels
{
    public class UserTakingPart : TakingPart
    {
        public string UserId { get; set; } = default!;
        public AppUser AppUser { get; set; } = default!;
    }
}