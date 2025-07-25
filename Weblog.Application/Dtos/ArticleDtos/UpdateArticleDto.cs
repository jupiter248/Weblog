using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblog.Application.Dtos.ArticleDtos
{
    public class UpdateArticleDto
    {
        public required string Title { get; set; }
        public required string Context { get; set; }
        public required string Description { get; set; }
        public required int CategoryId { get; set; }
        public bool IsPublished { get; set; } = false;
    }
}