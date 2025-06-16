using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblog.Application.Queries
{
    public class PaginationParams
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 6;
    }
}