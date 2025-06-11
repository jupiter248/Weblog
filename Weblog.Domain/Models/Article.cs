using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Weblog.Domain.Models
{
    public class Article
    {
        [Key]
        public int Id { get; set; }
        public required string Slug { get; set; }
        public required string Context { get; set; }
        public int Viewers { get; set; } = 1;
        public string? Author { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public required bool IsPublished { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        public DateTime? PublishedAt { get; set; }
        public List<Medium> Media { get; set; } = new List<Medium>();
    }
}