using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Weblog.Domain.Enums;
using Weblog.Domain.Models;

namespace Weblog.Domain.JoinModels.Favorites
{
    public class FavoriteList
    {
        [Key]
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public required string UserId { get; set; }
        public AppUser? AppUser { get; set; }
        public List<Article> Articles { get; set; } = new List<Article>();
        public List<Event> Events { get; set; } = new List<Event>();
        public List<Podcast> Podcasts { get; set; } = new List<Podcast>();
    }
}