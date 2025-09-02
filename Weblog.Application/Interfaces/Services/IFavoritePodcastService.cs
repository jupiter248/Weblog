using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblog.Application.Dtos.FavoritesDtos.PodcastFavoriteDto;
using Weblog.Application.Dtos.PodcastDtos;
using Weblog.Application.Queries;
using Weblog.Application.Queries.FilteringParams;

namespace Weblog.Application.Interfaces.Services
{
    public interface IFavoritePodcastService
    {
        Task<List<PodcastSummaryDto>> GetAllFavoritePodcastsAsync(string userId ,FavoriteFilteringParams favoriteFilteringParams , PaginationParams paginationParams);
        Task AddPodcastToFavoriteAsync(string userId,AddFavoritePodcastDto addFavoritePodcastDto);
        Task DeletePodcastFromFavoriteAsync(int podcastId , string userId);
    }
}