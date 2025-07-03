using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblog.Domain.Enums;

namespace Weblog.Application.Queries.FilteringParams
{
    public class CategoryFilteringParams
    {
        public EntityType? EntityType { get; set; }
    }
}