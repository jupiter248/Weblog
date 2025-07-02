using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Weblog.Domain.Models
{
    public class Contributor
    {
        [Key]
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string FamilyName { get; set; }
        public required string FullName { get; set; }
        public List<Article> Articles { get; set; } = new List<Article>();
        public List<Podcast> Podcasts { get; set; } = new List<Podcast>();
        public List<Medium> Media { get; set; } = new List<Medium>();
        public List<Event> Events { get; set; } = new List<Event>();

        

    }
}