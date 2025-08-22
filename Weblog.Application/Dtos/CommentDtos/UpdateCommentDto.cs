using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Weblog.Application.Dtos.CommentDtos
{
    public class UpdateCommentDto
    {
        public required string Text { get; set; }
    }
}