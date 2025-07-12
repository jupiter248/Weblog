using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Weblog.Application.Interfaces.Repositories;
using Weblog.Domain.JoinModels.Favorites;
using Weblog.Persistence.Data;

namespace Weblog.Persistence.Repositories
{
    public class FavoriteListRepository : IFavoriteListRepository
    {
        private readonly ApplicationDbContext _context;
        public FavoriteListRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<FavoriteList> AddFavoriteListAsync(FavoriteList favoriteList)
        {
            await _context.FavoriteLists.AddAsync(favoriteList);
            await _context.SaveChangesAsync();
            return favoriteList;
        }

        public async Task DeleteFavoriteListAsync(FavoriteList favoriteList)
        {
            _context.FavoriteLists.Remove(favoriteList);
            await _context.SaveChangesAsync();
        }

        public async Task<List<FavoriteList>> GetAllFavoritesListAsync()
        {
            return await _context.FavoriteLists.Include(f => f.Articles).Include(f => f.Events).Include(f => f.Podcasts).ToListAsync();
        }

        public async Task<FavoriteList?> GetFavoriteListByIdAsync(int favoriteListId)
        {
            FavoriteList? favoriteList = await _context.FavoriteLists.FirstOrDefaultAsync(f => f.Id == favoriteListId);
            if (favoriteList == null)
            {
                return null;
            }
            return favoriteList;
        }

        public async Task UpdateFavoriteListAsync(FavoriteList favoriteList)
        {
            _context.FavoriteLists.Update(favoriteList);
            await _context.SaveChangesAsync();
        }
    }
}