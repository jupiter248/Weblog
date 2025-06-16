using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Weblog.Domain.Enums;

namespace Weblog.Domain.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required CategoryParentType CategoryParentType { get; set; }
        public List<Article> Articles { get; set; } = new List<Article>();
        public List<Event> Events { get; set; } = new List<Event>();
        public List<Podcast> Podcasts { get; set; } = new List<Podcast>();


    }
}