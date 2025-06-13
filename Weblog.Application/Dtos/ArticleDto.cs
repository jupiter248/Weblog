using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblog.Application.Dtos
{
    public class ArticleDto
    {
        public required string Title { get; set; }
        public required string Slug { get; set; }
        public required string Context { get; set; }
        public required int Viewers { get; set; }
        public required int CategoryId { get; set; }
        public required bool IsPublished { get; set; } 
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? PublishedAt { get; set; }

        //List media
        //List tags
        //List Authors
    }
}