using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblog.Domain.Enums;

namespace Weblog.Application.Dtos.CommentDtos
{
    public class AddCommentDto
    {
        public required string Text { get; set; }
        public CommentParentType EntityType { get; set; }
        public int EntityId { get; set; }
    }
}