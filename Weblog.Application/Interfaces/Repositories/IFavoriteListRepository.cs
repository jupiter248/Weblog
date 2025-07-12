using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblog.Domain.JoinModels.Favorites;

namespace Weblog.Application.Interfaces.Repositories
{
    public interface IFavoriteListRepository
    {
        Task<List<FavoriteList>> GetAllFavoritesListAsync();
        Task<FavoriteList> AddFavoriteListAsync(FavoriteList favoriteList);
        Task<FavoriteList?> GetFavoriteListByIdAsync(int favoriteListId);
        Task UpdateFavoriteListAsync(FavoriteList favoriteList);
        Task DeleteFavoriteListAsync(FavoriteList favoriteList);    
    }
}