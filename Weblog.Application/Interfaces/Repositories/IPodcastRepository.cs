using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblog.Application.Queries;
using Weblog.Application.Queries.FilteringParams;
using Weblog.Domain.Models;

namespace Weblog.Application.Interfaces.Repositories
{
    public interface IPodcastRepository
    {
        Task<List<Podcast>> GetAllPodcastsAsync(FilteringParams filteringParams, PaginationParams paginationParams);
        Task<Podcast?> GetPodcastByIdAsync(int podcastId);
        Task<Podcast> AddPodcastAsync(Podcast podcast);
        Task UpdatePodcastAsync(Podcast podcast);
        Task DeletePodcastAsync(Podcast podcast);
        Task<List<Podcast>> SearchByTitleAsync(string keyword);
        Task AddTagAsync(Podcast podcast, Tag tag);
        Task DeleteTagAsync(Podcast podcast, Tag tag);
        Task AddContributorAsync(Podcast podcast, Contributor contributor);
        Task DeleteContributorAsync(Podcast podcast, Contributor contributor);
        Task<bool> PodcastExistsAsync(int podcastId);
        Task<List<Podcast>> GetSuggestionsAsync(PaginationParams paginationParams, Podcast podcast);

    }
}