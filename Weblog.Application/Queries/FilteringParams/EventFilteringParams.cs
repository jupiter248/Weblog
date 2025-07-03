using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblog.Application.Queries.FilteringParams
{
    public class EventFilteringParams
    {
        public int? CategoryId { get; set; }
        public string? Place {get;set;}
    }
}