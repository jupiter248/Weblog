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

            var results = new List<SearchResultDto>();

            if (request.Type == null || request.Type == CategoryParentType.Article)
            {
                var articleResults = await _articleRepo.SearchByTitleAsync(keyword);
                results.AddRange(articleResults.Select(a => new SearchResultDto
                {
                    categoryParentType = CategoryParentType.Article,
                    ParentId = a.Id,
                    Title = a.Title,
                    Description = a.Description,
                }));
            }

            if (request.Type == null || request.Type == CategoryParentType.Podcast)
            {
                var podcastResults = await _podcastRepo.SearchByTitleAsync(keyword);
                results.AddRange(podcastResults.Select(a => new SearchResultDto
                {
                    categoryParentType = CategoryParentType.Podcast,
                    ParentId = a.Id,
                    Title = a.Name,
                    Description = a.Description,
                }));
            }

            if (request.Type == null || request.Type == CategoryParentType.Event)
            {
                var eventResults = await _eventRepo.SearchByTitleAsync(keyword);
                results.AddRange(eventResults.Select(a => new SearchResultDto
                {
                    categoryParentType = CategoryParentType.Event,
                    ParentId = a.Id,
                    Title = a.Title,
                    Description = a.Description,
                }));
            }

            return results.ToList();
        }
    }
}