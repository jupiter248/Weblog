using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Weblog.Application.Dtos.TagDtos
{
    public class UpdateTagDto
    {
        [MaxLength(25 , ErrorMessage = "Title can not be more than 25 char")]
        public required string Title{ get; set; }
    }
}