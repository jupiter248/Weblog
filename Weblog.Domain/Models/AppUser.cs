using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Weblog.Domain.JoinModels;
using Weblog.Domain.JoinModels.Favorites;
using Weblog.Domain.Models;

namespace Weblog.Domain.Models
{
    public class AppUser : IdentityUser
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string FullName { get; set; }
        public  DateTimeOffset CreatedAt { get; set; }
        public  DateTimeOffset UpdatedAt { get; set; }
        public List<Comment> Comments { get; set; } = new List<Comment>();
        public List<Medium> Media { get; set; } = new List<Medium>();
        public List<FavoriteArticle> FavoriteArticles { get; set; } = new List<FavoriteArticle>();
        public List<FavoriteEvent> FavoriteEvents { get; set; } = new List<FavoriteEvent>();
        public List<FavoritePodcast> FavoritePodcasts { get; set; } = new List<FavoritePodcast>();
        public List<TakingPart> TakingParts { get; set; } = new List<TakingPart>();

    }
}