using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblog.Domain.Enums;

namespace Weblog.Application.Queries
{
    public class FilteringCategoryParams
    {
        public CategoryParentType? CategoryParentType { get; set; }
    }
}