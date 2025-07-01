using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Weblog.Application.Dtos.SearchResultDto;

namespace Weblog.Application.Features
{
    public class SearchContentQuery : IRequest<List<SearchResultDto>>
    {
        public string Keyword { get; set; }

        public SearchContentQuery(string keyword)
        {
            Keyword = keyword;
        }
    }
}