using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblog.Application.Queries;
using Weblog.Domain.Models;

namespace Weblog.Application.Interfaces.Repositories
{
    public interface IEventRepository
    {
        Task<List<Event>> GetAllEventsAsync(FilteringParams filteringParams, PaginationParams paginationParams);
        Task<Event?> GetEventByIdAsync(int eventId);
        Task<Event> AddEventAsync(Event eventModel);
        Task UpdateEventAsync(Event eventModel);
        Task UpdateLikesAsync(Event eventModel);
        Task UpdateViewersAsync(Event eventModel);
        Task DeleteEventAsync(Event eventModel);
        Task AddTagToEvent(Event eventModel, Tag tag);
        Task DeleteTagFromEvent(Event eventModel, Tag tag);
        
    }
}