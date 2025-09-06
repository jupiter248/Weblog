using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblog.Domain.Enums;

namespace Weblog.Application.Dtos.SearchResultDto
{
    public class SearchResultDto
    {
        public int ParentId { get; set; }
        public required string Title { get; set; }
        public required SearchType EntityType { get; set; }
        public string? Description { get; set; }
    }
}