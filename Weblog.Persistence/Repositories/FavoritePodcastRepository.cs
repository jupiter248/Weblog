using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Weblog.Application.Interfaces.Repositories;
using Weblog.Application.Queries;
using Weblog.Application.Queries.FilteringParams;
using Weblog.Domain.JoinModels;
using Weblog.Persistence.Data;

namespace Weblog.Persistence.Repositories
{
    public class FavoritePodcastRepository : IFavoritePodcastRepository
    {
         private readonly ApplicationDbContext _context;
        public FavoritePodcastRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddPodcastToFavoriteAsync(FavoritePodcast favoritePodcast)
        {
            await _context.FavoritePodcasts.AddAsync(favoritePodcast);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePodcastFromFavoriteAsync(FavoritePodcast favoritePodcast)
        {
            _context.FavoritePodcasts.Remove(favoritePodcast);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsPodcastFavoriteAsync(FavoritePodcast favoritePodcast)
        {
            FavoritePodcast? favoritePodcastExists = await _context.FavoritePodcasts.FirstOrDefaultAsync(f => f.UserId == favoritePodcast.UserId && f.PodcastId == favoritePodcast.PodcastId);
            if (favoritePodcastExists == null)
            {
                return false;
            }
            return true;
        }

        public async Task<List<FavoritePodcast>> GetAllFavoritePodcastsAsync(string userId, FavoriteFilteringParams favoriteFilteringParams, PaginationParams paginationParams)
        {
            var favoritePodcastQuery = _context.FavoritePodcasts.Include(a => a.Podcast).ThenInclude(m => m.Media).Where(a => a.UserId == userId).AsQueryable();
            if (favoriteFilteringParams.FavoriteListId.HasValue)
            {
                favoritePodcastQuery = favoritePodcastQuery.Where(f => f.FavoriteListId == favoriteFilteringParams.FavoriteListId);
            }

            if (favoriteFilteringParams.NewestArrivals == true)
            {
                favoritePodcastQuery = favoritePodcastQuery.OrderByDescending(a => a.AddedAt);
            }
            else
            {
                favoritePodcastQuery = favoritePodcastQuery.OrderBy(a => a.AddedAt);
            }

            List<FavoritePodcast> favoritePodcasts = await favoritePodcastQuery.ToListAsync();
            var skipNumber = (paginationParams.PageNumber - 1) * paginationParams.PageSize;

            return favoritePodcastQuery.Skip(skipNumber).Take(paginationParams.PageSize).ToList(); 
        }

        public async Task<FavoritePodcast?> GetFavoritePodcastByIdAsync(int id)
        {
            FavoritePodcast? favoritePodcast = await _context.FavoritePodcasts.FirstOrDefaultAsync(f => f.Id == id);
            if (favoritePodcast == null)
            {
                return null;
            }
            return favoritePodcast;
        }
    }
}