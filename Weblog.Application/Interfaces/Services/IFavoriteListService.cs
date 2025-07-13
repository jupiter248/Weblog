using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblog.Application.Dtos.FavoriteListDtos;

namespace Weblog.Application.Interfaces.Services
{
    public interface IFavoriteListService
    {
        Task<List<FavoriteListDto>> GetAllFavoriteListsAsync(string userId);
        Task<FavoriteListDto> GetFavoriteListByIdAsync(int favoriteListId);
        Task<FavoriteListDto> AddFavoriteListAsync(string userId ,AddFavoriteListDto addFavoriteListDto);
        Task UpdateFavoriteListAsync( string userId,UpdateFavoriteListDto updateFavoriteListDto , int favoriteListId);
        Task DeleteFavoriteList(string userId,int favoriteListId);


    }
}