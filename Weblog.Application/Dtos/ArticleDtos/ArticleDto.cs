using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblog.Application.Dtos.ArticleDtos
{
    public class ArticleDto
    {
        public int Id { get; set;   }
        public required string Title { get; set; }
        public required string Slug { get; set; }
        public required string Context { get; set; }
        public required int Viewers { get; set; }
        public required int Likes { get; set; }
        public required int CategoryId { get; set; }
        public required bool IsPublished { get; set; } 
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public DateTimeOffset? PublishedAt { get; set; }

        //List media
        //List tags
        //List Authors
    }
}