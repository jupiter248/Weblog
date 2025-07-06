using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblog.Application.Dtos.CommentDtos
{
    public class CommentDto
    {
        public int Id { get; set; }
        public required string Text { get; set; }
        public required string FullName { get; set; }
    }
}