using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblog.Domain.Enums;

namespace Weblog.Domain.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public required string Text { get; set; }
        public CommentType EntityType { get; set; }
        public int EntityId { get; set; }
        public required string UserId { get; set; }   
        public AppUser? AppUser { get; set; }     
    }
}