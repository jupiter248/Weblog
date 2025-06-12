using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Weblog.Domain.Enums;

namespace Weblog.Domain.Models
{
    public class Medium
    {
        [Key]
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Path { get; set; }
        public MediumType MediumType { get; set; }
        public MediumParentType MediumParentType { get; set; }
        public int? ArticleId { get; set; }
        public Article? Article { get; set; } 
        public int? EventId { get; set; }
        public Event? Event { get; set; }
        public int? PodcastId { get; set; }
        public Podcast? Podcast { get; set; }  
        public int? ContributorId { get; set; }
        public Contributor? Contributor { get; set; } 
        public bool IsPrimary { get; set; } = false;
    }
}