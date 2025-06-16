using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblog.Domain.Enums;

namespace Weblog.Application.Dtos.CategoryDtos
{
    public class CategoryDto
    {
        public int Id { get; set;   }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required CategoryParentType CategoryParentType { get; set; }
    }
}