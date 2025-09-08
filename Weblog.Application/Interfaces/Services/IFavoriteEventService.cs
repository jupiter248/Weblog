using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblog.Application.Dtos.EventDtos;
using Weblog.Application.Dtos.FavoritesDtos.EventFavoriteDtos;
using Weblog.Application.Queries;
using Weblog.Application.Queries.FilteringParams;

namespace Weblog.Application.Interfaces.Services
{
    public interface IFavoriteEventService
    {
        Task<List<EventSummaryDto>> GetAllFavoriteEventsAsync(string userId, FavoriteFilteringParams favoriteFilteringParams, PaginationParams paginationParams);
        Task AddEventToFavoriteAsync(string userId, AddFavoriteEventDto addFavoriteEventDto);
        Task DeleteEventFromFavoriteAsync(int eventId, string userId);
        Task<bool> IsEventFavoriteAsync(string userId , int eventId);

    }
}