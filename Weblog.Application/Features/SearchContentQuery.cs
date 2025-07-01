using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Weblog.Application.Dtos.SearchResultDto;
using Weblog.Domain.Enums;
using Weblog.Domain.Models;

namespace Weblog.Application.Features
{
    public class SearchContentQuery : IRequest<List<SearchResultDto>>
    {
        public string Keyword { get; set; }
        public CategoryParentType? Type { get; set; } // "Article", "Podcast", "Event"

        public SearchContentQuery(string keyword , CategoryParentType type)
        {
            Keyword = keyword;
            Type = type;
        }
    }
}