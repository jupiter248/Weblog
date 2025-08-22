using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Weblog.Application.Dtos.TagDtos
{
    public class UpdateTagDto
    {
        public required string Title{ get; set; }
    }
}