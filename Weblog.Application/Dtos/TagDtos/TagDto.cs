using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblog.Application.Dtos.TagDtos
{
    public class TagDto
    {
        public int Id { get; set; }
        public required string Title{ get; set; }
    }
}