using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Weblog.Application.Dtos.EventDtos;
using Weblog.Application.Interfaces.Repositories;
using Weblog.Application.Interfaces.Services;
using Weblog.Application.Queries;
using Weblog.Application.Queries.FilteringParams;

namespace Weblog.API.Controllers
{
    [ApiController]
    [Route("api/event")]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;
        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllEvents([FromQuery] EventFilteringParams eventFilteringParams, [FromQuery] PaginationParams paginationParams)
        {
            List<EventDto> eventDtos = await _eventService.GetAllEventsAsync(paginationParams, eventFilteringParams);
            return Ok(eventDtos);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetEventById(int id)
        {
            EventDto eventDto = await _eventService.GetEventByIdAsync(id);
            return Ok(eventDto);
        }
        [HttpPost]
        public async Task<IActionResult> AddEvent([FromBody] AddEventDto addEventDto)
        {
            EventDto eventDto = await _eventService.AddEventAsync(addEventDto);
            return CreatedAtAction(nameof(GetEventById), new { id = eventDto.Id }, eventDto);
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateEvent(int id, UpdateEventDto updateEventDto)
        {
            await _eventService.UpdateEventAsync(updateEventDto, id);
            return NoContent();
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            await _eventService.DeleteEventAsync(id);
            return NoContent();
        }
        [HttpPut("{id:int}/viewer")]
        public async Task<IActionResult> UpdateViewers(int id)
        {
            await _eventService.UpdateViewersAsync(id);
            return NoContent();
        }
        [HttpPut("{id:int}/like")]
        public async Task<IActionResult> UpdateLikes(int id)
        {
            await _eventService.UpdateLikesAsync(id);
            return NoContent();
        }
        [HttpPost("{id:int}/tag")]
        public async Task<IActionResult> AddTagToEvent(int id, int tagId)
        {
            await _eventService.AddTagAsync(id, tagId);
            return NoContent();
        }
        [HttpDelete("{id:int}/tag")]
        public async Task<IActionResult> DeleteTagOfEvent(int id, int tagId)
        {
            await _eventService.DeleteTagAsync(id, tagId);
            return NoContent();
        }
    }
}