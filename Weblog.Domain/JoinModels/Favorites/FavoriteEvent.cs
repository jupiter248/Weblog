using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblog.Domain.Models;

namespace Weblog.Domain.JoinModels
{
    public class FavoriteEvent
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public Event? Event { get; set; }
        public required string UserId { get; set; }
        public AppUser? AppUser { get; set; }
    }
}