using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Weblog.Application.Dtos.CommentDtos
{
    public class UpdateCommentDto
    {
        [MaxLength(250 , ErrorMessage = "Text can't be more that 250 char")]
        public required string Text { get; set; }    }
}