using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblog.Application.Dtos.EventDtos;
using Weblog.Application.Queries;
using Weblog.Application.Queries.FilteringParams;

namespace Weblog.Application.Interfaces.Services
{
    public interface IEventService
    {
        Task<List<EventDto>> GetAllEventsAsync(PaginationParams paginationParams, EventFilteringParams eventFilteringParams);
        Task<EventDto> GetEventByIdAsync(int eventId);
        Task<EventDto> AddEventAsync(AddEventDto addEventDto);
        Task UpdateViewersAsync(int eventId);
        Task UpdateLikesAsync(int eventId);
        Task UpdateEventAsync(UpdateEventDto updateEventDto, int eventId);
        Task DeleteEventAsync(int eventId);
        Task AddTagAsync(int eventId, int tagId);
        Task DeleteTagAsync(int eventId, int tagId);
        Task AddContributorAsync(int eventId , int contributorId);
        Task DeleteContributorAsync(int eventId , int contributorId);
    }
}