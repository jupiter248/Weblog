using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblog.Domain.JoinModels;
using Weblog.Domain.Models;

namespace Weblog.Application.Interfaces.Repositories
{
    public interface IFavoriteEventRepository
    {
        Task<List<FavoriteEvent>> GetAllFavoriteEventsAsync(string userId);
        Task AddEventToFavoriteAsync(FavoriteEvent favoriteEvent);
        Task DeleteEventFromFavoriteAsync(FavoriteEvent favoriteEvent);
        Task<bool> EventAddedToFavoriteAsync(FavoriteEvent favoriteArticle);
        Task<FavoriteEvent?> GetFavoriteEventByIdAsync(int id);
    }
}