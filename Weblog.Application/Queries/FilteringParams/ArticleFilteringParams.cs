using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblog.Application.Queries.FilteringParams
{
    public class ArticleFilteringParams
    {
        public int? CategoryId { get; set; }
        public bool NewestArrivals { get; set; } = false;
        public bool? IsPublished { get; set; } 



    }
}