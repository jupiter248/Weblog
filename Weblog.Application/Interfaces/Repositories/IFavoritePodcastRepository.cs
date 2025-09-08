using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblog.Application.Queries;
using Weblog.Application.Queries.FilteringParams;
using Weblog.Domain.JoinModels;
using Weblog.Domain.Models;

namespace Weblog.Application.Interfaces.Repositories
{
    public interface IFavoritePodcastRepository
    {
        Task<List<FavoritePodcast>> GetAllFavoritePodcastsAsync(string userId , FavoriteFilteringParams favoriteFilteringParams , PaginationParams paginationParams);
        Task AddPodcastToFavoriteAsync(FavoritePodcast favoritePodcast);
        Task DeletePodcastFromFavoriteAsync(FavoritePodcast favoritePodcast);
        Task<bool> IsPodcastFavoriteAsync(FavoritePodcast favoritePodcast);
        Task<FavoritePodcast?> GetFavoritePodcastByIdAsync(int id);
    }
}