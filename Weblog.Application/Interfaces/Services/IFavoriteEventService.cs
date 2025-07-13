using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblog.Application.Dtos.EventDtos;
using Weblog.Application.Dtos.FavoritesDtos.EventFavoriteDtos;

namespace Weblog.Application.Interfaces.Services
{
    public interface IFavoriteEventService
    {
        Task<List<EventDto>> GetAllFavoriteEventsAsync(string userId , int? favoriteListId);
        Task AddEventToFavoriteAsync(string userId,AddFavoriteEventDto addFavoriteEventDto);
        Task DeleteEventFromFavoriteAsync(int eventId , string userId);
    }
}