using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblog.Application.Queries;
using Weblog.Application.Queries.FilteringParams;
using Weblog.Domain.Models;

namespace Weblog.Application.Interfaces.Repositories
{
    public interface IEventRepository
    {
        Task<List<Event>> GetAllEventsAsync(EventFilteringParams eventFilteringParams, PaginationParams paginationParams);
        Task<Event?> GetEventByIdAsync(int eventId);
        Task<Event> AddEventAsync(Event eventModel);
        Task UpdateEventAsync(Event eventModel);
        Task DeleteEventAsync(Event eventModel);
        Task<List<Event>> SearchByTitleAsync(string keyword);
        Task AddTagToEvent(Event eventModel, Tag tag);
        Task DeleteTagFromEvent(Event eventModel, Tag tag);
        Task AddContributorAsync(Event eventModel, Contributor contributor);
        Task DeleteContributorAsync(Event eventModel, Contributor contributor);
        Task<bool> EventExistsAsync(int eventId);
        Task IncreaseCapacity(Event eventModel);
        Task<int?> DecreaseCapacity(Event eventModel);
        Task<List<Event>> GetSuggestionsAsync(PaginationParams paginationParams, Event eventModel);

    }
}