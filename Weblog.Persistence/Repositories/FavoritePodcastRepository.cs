using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Weblog.Application.Interfaces.Repositories;
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

        public async Task<bool> PodcastAddedToFavoriteAsync(FavoritePodcast favoritePodcast)
        {
            FavoritePodcast? favoritePodcastExists = await _context.FavoritePodcasts.FirstOrDefaultAsync(f => f.UserId == favoritePodcast.UserId && f.PodcastId == favoritePodcast.PodcastId);
            if (favoritePodcastExists == null)
            {
                return false;
            }
            return true;
        }

        public async Task<List<FavoritePodcast>> GetAllFavoritePodcastsAsync(string userId)
        {
            return await _context.FavoritePodcasts.Include(a => a.Podcast).Where(a => a.UserId == userId).ToListAsync();
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