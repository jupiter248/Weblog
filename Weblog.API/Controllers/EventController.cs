using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Weblog.Application.Dtos.EventDtos;
using Weblog.Application.Extensions;
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
        private readonly IFavoriteEventService _favoriteEventService;
        private readonly ITakingPartService _takingPartService;


        public EventController(ITakingPartService takingPartService, IEventService eventService, IFavoriteEventService favoriteEventService)
        {
            _eventService = eventService;
            _favoriteEventService = favoriteEventService;
            _takingPartService = takingPartService;
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
        [HttpPost("{id:int}/contributor")]
        public async Task<IActionResult> AddContributorToEvent(int id, int contributorId)
        {
            await _eventService.AddContributorAsync(id, contributorId);
            return NoContent();
        }
        [HttpDelete("{id:int}/contributor")]
        public async Task<IActionResult> DeleteContributorOfEvent(int id, int contributorId)
        {
            await _eventService.DeleteContributorAsync(id, contributorId);
            return NoContent();
        }
        [HttpPost("{id:int}/favorite")]
        public async Task<IActionResult> AddArticleToFavorite(int id)
        {
            string? userId = User.GetUserId();
            if (string.IsNullOrWhiteSpace(userId)) return BadRequest("UserId is invalid");
            await _favoriteEventService.AddEventToFavoriteAsync(id, userId);
            return NoContent();
        }
        [HttpDelete("{id:int}/favorite")]
        public async Task<IActionResult> DeleteArticleOfFavorite(int id)
        {
            string? userId = User.GetUserId();
            if (string.IsNullOrWhiteSpace(userId)) return BadRequest("UserId is invalid");
            await _favoriteEventService.DeleteEventFromFavoriteAsync(id, userId);
            return NoContent();
        }
        [HttpPost("{id:int}/take-part")]
        public async Task<IActionResult> TakePart(int id)
        {
            string? userId = User.GetUserId();
            if (string.IsNullOrWhiteSpace(userId)) return BadRequest("UserId is invalid");
            await _takingPartService.TakePartAsync(id, userId);
            return NoContent();
        }
        [HttpGet("{id:int}/participants")]
        public async Task<IActionResult> GetAllParticipantsByEventId(int id)
        {
            List<ParticipantDto> participantDtos =  await _takingPartService.GetAllParticipantsAsync(id);
            return Ok(participantDtos);
        }
        [HttpPut("{id:int}/taking-part")]
        public async Task<IActionResult> UpdateTakingPart(int id, bool isConfirmed)
        {
            await _takingPartService.UpdateTakingPartAsync(id, isConfirmed);
            return NoContent();
        }
        [HttpDelete("{id:int}/cancel-taking-part")]
        public async Task<IActionResult> CancelTakingPart(int id)
        {
            string? userId = User.GetUserId();
            if (string.IsNullOrWhiteSpace(userId)) return BadRequest("UserId is invalid");
            await _takingPartService.CancelTakingPartAsync(id, userId);
            return NoContent();
        }
    }
}