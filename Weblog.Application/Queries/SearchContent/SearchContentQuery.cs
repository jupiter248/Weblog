using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Weblog.Application.Dtos.SearchResultDto;
using Weblog.Domain.Enums;
using Weblog.Domain.Models;

namespace Weblog.Application.Queries.SearchContent
{
    public class SearchContentQuery : IRequest<List<SearchResultDto>>
    {
        public string Keyword { get; set; }
        public EntityType? EntityType { get; set; } // "Article", "Podcast", "Event" ,"Person"

        public SearchContentQuery(string keyword , EntityType entityType)
        {
            Keyword = keyword;
            EntityType = entityType;
        }
    }
}