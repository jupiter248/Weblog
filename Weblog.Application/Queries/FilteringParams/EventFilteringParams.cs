using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblog.Application.Queries.FilteringParams
{
    public class EventFilteringParams
    {
        public int? CategoryId { get; set; }
        public string? Place { get; set; }
        public bool NewestArrivals { get; set; } = false;
        public bool? IsPublished { get; set; }
        public int? TagId { get; set; }

    }
}