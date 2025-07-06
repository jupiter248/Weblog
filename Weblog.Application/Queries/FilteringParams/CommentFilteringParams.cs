using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblog.Domain.Enums;

namespace Weblog.Application.Queries.FilteringParams
{
    public class CommentFilteringParams
    {
        public int? EntityId { get; set; }
        public CommentParentType? CommentParentType { get; set; }
    }
}