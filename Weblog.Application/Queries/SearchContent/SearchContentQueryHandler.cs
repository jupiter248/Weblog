using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Weblog.Application.Dtos.SearchResultDto;
using Weblog.Application.Interfaces.Repositories;
using Weblog.Domain.Enums;

namespace Weblog.Application.Queries.SearchContent
{
    public class SearchContentQueryHandler : IRequestHandler<SearchContentQuery, List<SearchResultDto>>
    {
        private readonly IArticleRepository _articleRepo;
        private readonly IPodcastRepository _podcastRepo;
        private readonly IEventRepository _eventRepo;
        private readonly IContributorRepository _contributorRepo;


        public SearchContentQueryHandler(
            IArticleRepository articleRepo,
            IPodcastRepository podcastRepo,
            IEventRepository eventRepo,
            IContributorRepository contributorRepo)
        {
            _articleRepo = articleRepo;
            _podcastRepo = podcastRepo;
            _eventRepo = eventRepo;
            _contributorRepo = contributorRepo;
        }
        public async Task<List<SearchResultDto>> Handle(SearchContentQuery request, CancellationToken cancellationToken)
        {
            var keyword = request.Keyword.ToLower();

            var results = new List<SearchResultDto>();

            if (request.EntityType == null || request.EntityType == EntityType.Article)
            {
                var articleResults = await _articleRepo.SearchByTitleAsync(keyword);
                results.AddRange(articleResults.Select(a => new SearchResultDto
                {
                    EntityType = EntityType.Article,
                    ParentId = a.Id,
                    Title = a.Title,
                    Description = a.Description,
                }));
            }

            if (request.EntityType == null || request.EntityType == EntityType.Podcast)
            {
                var podcastResults = await _podcastRepo.SearchByTitleAsync(keyword);
                results.AddRange(podcastResults.Select(a => new SearchResultDto
                {
                    EntityType = EntityType.Podcast,
                    ParentId = a.Id,
                    Title = a.Name,
                    Description = a.Description,
                }));
            }

            if (request.EntityType == null || request.EntityType == EntityType.Event)
            {
                var eventResults = await _eventRepo.SearchByTitleAsync(keyword);
                results.AddRange(eventResults.Select(a => new SearchResultDto
                {
                    EntityType = EntityType.Event,
                    ParentId = a.Id,
                    Title = a.Title,
                    Description = a.Description,
                }));
            }

            if (request.EntityType == null || request.EntityType == EntityType.Contributor)
            {
                var eventResults = await _contributorRepo.SearchByNameAsync(keyword);
                results.AddRange(eventResults.Select(a => new SearchResultDto
                {
                    EntityType = EntityType.Event,
                    ParentId = a.Id,
                    Title = a.FullName,
                }));
            }

            return results.ToList();
        }
    }
}