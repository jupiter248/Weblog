using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblog.Domain.Models;

namespace Weblog.Domain.JoinModels
{
    public class FavoriteArticle
    {
        public int Id { get; set; }
        public int ArticleId { get; set; }
        public Article? Article { get; set; }
        public required string UserId { get; set; }
        public AppUser? AppUser { get; set; }
        public int? FavoriteListId { get; set; }
    }
}