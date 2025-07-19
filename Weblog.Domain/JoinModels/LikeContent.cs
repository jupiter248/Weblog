using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblog.Domain.Enums;
using Weblog.Domain.Models;

namespace Weblog.Domain.JoinModels
{
    public class LikeContent
    {
        public int Id { get; set; }
        public int EntityId { get; set; }
        public LikeAndViewType EntityType { get; set; }
        public required string UserId { get; set; }
        public AppUser? AppUser { get; set; }
    }
}