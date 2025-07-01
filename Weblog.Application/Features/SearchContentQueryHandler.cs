using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Weblog.Application.Dtos.SearchResultDto;
using Weblog.Application.Interfaces.Repositories;
using Weblog.Domain.Enums;

namespace Weblog.Application.Features
{
    public class SearchContentQueryHandler : IRequestHandler<SearchContentQuery, List<SearchResultDto>>
    {
        private readonly IArticleRepository _articleRepo;
        private readonly IPodcastRepository _podcastRepo;
        private readonly IEventRepository _eventRepo;

        public SearchContentQueryHandler(
            IArticleRepository articleRepo,
            IPodcastRepository podcastRepo,
            IEventRepository eventRepo)
        {
            _articleRepo = articleRepo;
            _podcastRepo = podcastRepo;
            _eventRepo = eventRepo;
        }
        public async Task<List<SearchResultDto>> Handle(SearchContentQuery request, CancellationToken cancellationToken)
        {
            var keyword = request.Keyword.ToLower();

            var articleResults = await _articleRepo.SearchByTitleAsync(keyword);
            var podcastResults = await _podcastRepo.SearchByTitleAsync(keyword);
            var eventResults = await _eventRepo.SearchByTitleAsync(keyword);

            var results = new List<SearchResultDto>();

            results.AddRange(articleResults.Select(a => new SearchResultDto
            {
                categoryParentType = CategoryParentType.Article,
                ParentId = a.Id,
                Title = a.Title,
                Description = a.Description,
            }));

            results.AddRange(podcastResults.Select(p => new SearchResultDto
            {
                categoryParentType = CategoryParentType.Article,
                ParentId = p.Id,
                Title = p.Name,
                Description = p.Description,
            }));

            results.AddRange(eventResults.Select(e => new SearchResultDto
            {
                categoryParentType = CategoryParentType.Article,
                ParentId = e.Id,
                Title = e.Title,
                Description = e.Description,
            }));

            return results.ToList();
        }
    }
}