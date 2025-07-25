using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Weblog.Domain.JoinModels;

namespace Weblog.Domain.Models
{
    public class Article
    {
        [Key]
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Slug { get; set; }
        public required string Context { get; set; }
        public required string Description { get; set; }
        public int CategoryId { get; set; }
        public required Category Category { get; set; }
        public bool IsPublished { get; set; } = false;
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
        public DateTimeOffset? UpdatedAt { get; set; }
        public DateTimeOffset? PublishedAt { get; set; }
        public List<Medium> Media { get; set; } = new List<Medium>();
        public List<Tag> Tags { get; set; } = new List<Tag>();
        public List<Contributor> Contributors { get; set; } = new List<Contributor>(); // The authors of article
        public List<Comment> Comments { get; set; } = new List<Comment>();
        public List<FavoriteArticle> FavoriteArticles { get; set; } = new List<FavoriteArticle>();
        public List<LikeContent> LikeContents { get; set; } = new List<LikeContent>();
        public List<ViewContent> ViewContents { get; set; } = new List<ViewContent>();


    }
}