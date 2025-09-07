using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Weblog.Domain.Enums;
using Weblog.Domain.JoinModels;

namespace Weblog.Domain.Models
{
    public class Podcast
    {
        [Key]
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Slug { get; set; }
        public required string Description { get; set; }
        public int CategoryId { get; set; }
        public required Category Category { get; set; }
        public required string Link { get; set; }
        public required bool IsDisplayed { get; set; } = false;
        public required int ViewCount { get; set; } = 0;
        public ContributorType ContributorType { get; set; }
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
        public DateTimeOffset DisplayedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public List<Medium> Media { get; set; } = new List<Medium>();
        public List<Tag> Tags { get; set; } = new List<Tag>();
        public List<Contributor> Contributors { get; set; } = new List<Contributor>(); // The hosts of podcast
        public List<Comment> Comments { get; set; } = new List<Comment>();
        public List<FavoritePodcast> FavoritePodcasts { get; set; } = new List<FavoritePodcast>();
        public List<LikeContent> LikeContents { get; set; } = new List<LikeContent>();
    }
}