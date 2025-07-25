using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Weblog.Application.Dtos.ArticleDtos
{
    public class AddArticleDto
    {
        [MaxLength(50 , ErrorMessage = "Title can not be more than 50 char")]
        public required string Title { get; set; }
        [MinLength(50 , ErrorMessage = "Context can not be less than 50 char")]
        public required string Context { get; set; }
        [MinLength(25 , ErrorMessage = "Description can not be less than 25 char")]
        public required string Description { get; set; }
        public required int CategoryId { get; set; }
        public bool IsPublished { get; set; }
        public AddArticleDto()
        {
            this.IsPublished = false;
        }
    }
}