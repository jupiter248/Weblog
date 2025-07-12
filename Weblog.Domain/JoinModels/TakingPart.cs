using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblog.Domain.Models;

namespace Weblog.Domain.JoinModels
{
    public class TakingPart
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public Event? Event { get; set; }
        public required string UserId { get; set; }
        public required AppUser AppUser { get; set; }
        public bool IsConfirmed { get; set; }
        public DateTimeOffset ConfirmedAt { get; set; }
        public DateTimeOffset AskedAt { get; set; }
        
    }
}