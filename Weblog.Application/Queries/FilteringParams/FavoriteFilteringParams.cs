using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblog.Application.Queries.FilteringParams
{
    public class FavoriteFilteringParams
    {
        public int? favoriteListId { get; set; }
        public bool NewestArrivals { get; set; } = false;
    }
}