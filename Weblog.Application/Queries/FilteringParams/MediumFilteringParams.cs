using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblog.Application.Queries.FilteringParams
{
    public class MediumFilteringParams
    {
        public bool IsPrimary { get; set; } = false;
        public bool IsOnPoster { get; set; } = false;
    }
}