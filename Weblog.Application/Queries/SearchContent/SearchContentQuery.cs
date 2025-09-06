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
        public SearchType? EntityType { get; set; } // "Article", "Podcast", "Event" ,"Person"

        public SearchContentQuery(string keyword , SearchType? entityType)
        {
            Keyword = keyword;
            EntityType = entityType;
        }
    }
}