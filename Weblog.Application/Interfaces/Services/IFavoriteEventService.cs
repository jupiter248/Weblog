using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblog.Application.Dtos.EventDtos;

namespace Weblog.Application.Interfaces.Services
{
    public interface IFavoriteEventService
    {
        Task<List<EventDto>> GetAllFavoriteEventsAsync(string userId);
        Task AddEventToFavoriteAsync(int eventId , string userId);
        Task DeleteEventFromFavoriteAsync(int eventId , string userId);
    }
}