using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblog.Application.Dtos.PodcastDtos;

namespace Weblog.Application.Interfaces.Services
{
    public interface IFavoritePodcastService
    {
        Task<List<PodcastDto>> GetAllFavoritePodcastsAsync(string userId);
        Task AddPodcastToFavoriteAsync(int podcastId , string userId);
        Task DeletePodcastFromFavoriteAsync(int podcastId , string userId);
    }
}