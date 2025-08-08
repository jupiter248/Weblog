using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblog.Application.Dtos.PodcastDtos;
using Weblog.Application.Queries;
using Weblog.Application.Queries.FilteringParams;

namespace Weblog.Application.Interfaces.Services
{
    public interface IPodcastService
    {
        Task<List<PodcastSummaryDto>> GetAllPodcastsAsync(PaginationParams paginationParams, PodcastFilteringParams podcastFilteringParams);
        Task<PodcastDto> GetPodcastByIdAsync(int podcastId);
        Task<PodcastDto> AddPodcastAsync(AddPodcastDto addPodcastDto);
        Task UpdatePodcastAsync(UpdatePodcastDto updatePodcastDto, int podcastId);
        Task DeletePodcastAsync(int podcastId);
        Task AddTagAsync(int podcastId, int tagId);
        Task DeleteTagAsync(int podcastId, int tagId);
        Task AddContributorAsync(int podcastId, int contributorId);
        Task DeleteContributorAsync(int podcastId, int contributorId);
        Task<List<PodcastSummaryDto>> GetSuggestionsAsync(PaginationParams paginationParams, int podcastId);

    }
}