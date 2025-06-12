using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Weblog.Domain.Models
{
    public class Podcast
    {
        [Key]
        public int Id { get; set; }
        public required string Slug { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string Link { get; set; }
        public string? MadeBy { get; set; }
        public List<Medium> Media { get; set; } = new List<Medium>();
        public List<Tag> Tags { get; set; } = new List<Tag>();
        public List<Contributor> Contributors { get; set; } = new List<Contributor>(); // The hosts of podcast


        
    }
}