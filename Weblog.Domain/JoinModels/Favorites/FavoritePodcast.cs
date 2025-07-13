using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblog.Domain.Enums;
using Weblog.Domain.JoinModels.Favorites;
using Weblog.Domain.Models;

namespace Weblog.Domain.JoinModels
{
    public class FavoritePodcast
    {
        public int Id { get; set; }
        public int PodcastId { get; set; }
        public Podcast? Podcast { get; set; }
        public required string UserId { get; set; }
        public AppUser? AppUser { get; set; }
        public int? FavoriteListId { get; set; }
    }
}