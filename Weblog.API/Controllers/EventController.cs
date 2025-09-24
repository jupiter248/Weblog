using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Weblog.Application.Dtos.EventDtos;
using Weblog.Application.Dtos.FavoritesDtos.EventFavoriteDtos;
using Weblog.Application.Dtos.PodcastDtos;
using Weblog.Application.Extensions;
using Weblog.Application.Interfaces.Repositories;
using Weblog.Application.Interfaces.Services;
using Weblog.Application.Queries;
using Weblog.Application.Queries.FilteringParams;
using Weblog.Application.Validations;
using Weblog.Application.Validations.Event;

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
            List<EventSummaryDto> eventSummaryDtos = await _eventService.GetAllEventsAsync(paginationParams, eventFilteringParams);
            return Ok(eventSummaryDtos);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetEventById(int id)
        {
            EventDto eventDto = await _eventService.GetEventByIdAsync(id);
            return Ok(eventDto);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddEvent([FromBody] AddEventDto addEventDto)
        {
            Validator.ValidateAndThrow(addEventDto, new AddEventValidator());
            EventDto eventDto = await _eventService.AddEventAsync(addEventDto);
            return CreatedAtAction(nameof(GetEventById), new { id = eventDto.Id }, eventDto);
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateEvent(int id, [FromBody] UpdateEventDto updateEventDto)
        {
            Validator.ValidateAndThrow(updateEventDto, new UpdateEventValidator());
            EventDto eventDto = await _eventService.UpdateEventAsync(updateEventDto, id);
            return Ok(eventDto);
        }
        [HttpPut("{eventId:int}/view")]
        public async Task<IActionResult> IncrementArticleView(int eventId)
        {
            int eventCount = await _eventService.IncrementEventViewAsync(eventId);
            return Ok(new
            {
                eventCount = eventCount
            });
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            await _eventService.DeleteEventAsync(id);
            return NoContent();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("{id:int}/tag")]
        public async Task<IActionResult> AddTagToEvent(int id, int tagId)
        {
            await _eventService.AddTagAsync(id, tagId);
            return NoContent();
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:int}/tag")]
        public async Task<IActionResult> DeleteTagOfEvent(int id, int tagId)
        {
            await _eventService.DeleteTagAsync(id, tagId);
            return NoContent();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("{id:int}/contributor")]
        public async Task<IActionResult> AddContributorToEvent(int id, int contributorId)
        {
            await _eventService.AddContributorAsync(id, contributorId);
            return NoContent();
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:int}/contributor")]
        public async Task<IActionResult> DeleteContributorOfEvent(int id, int contributorId)
        {
            await _eventService.DeleteContributorAsync(id, contributorId);
            return NoContent();
        }
        [Authorize]
        [HttpPost("favorite")]
        public async Task<IActionResult> AddEventToFavorite([FromBody] AddFavoriteEventDto addFavoriteEventDto)
        {
            string? userId = User.GetUserId();
            if (string.IsNullOrWhiteSpace(userId)) return BadRequest("UserId is invalid");
            await _favoriteEventService.AddEventToFavoriteAsync(userId, addFavoriteEventDto);
            return NoContent();
        }
        [Authorize()]
        [HttpGet("{eventId:int}/favorite-status")]
        public async Task<IActionResult> GetFavoriteStatus(int eventId)
        {
            string? userId = User.GetUserId();
            if (string.IsNullOrWhiteSpace(userId)) return BadRequest("UserId is invalid");
            bool isFavorite = await _favoriteEventService.IsEventFavoriteAsync(userId, eventId);
            return Ok(new
            {
                eventId = eventId,
                isFavorite = isFavorite
            });
        }
        [Authorize]
        [HttpGet("{eventId:int}/participant-status")]
        public async Task<IActionResult> GetParticipantStatus(int eventId)
        {
            string? userId = User.GetUserId();
            if (string.IsNullOrWhiteSpace(userId)) return BadRequest("UserId is invalid");
            bool isParticipant = await _takingPartService.IsUserParticipantAsync(userId, eventId);
            return Ok(new
            {
                eventId = eventId,
                isParticipant = isParticipant
            });
        }
        [Authorize]
        [HttpDelete("{id:int}/favorite")]
        public async Task<IActionResult> DeleteEventOfFavorite(int id)
        {
            string? userId = User.GetUserId();
            if (string.IsNullOrWhiteSpace(userId)) return BadRequest("UserId is invalid");
            await _favoriteEventService.DeleteEventFromFavoriteAsync(id, userId);
            return NoContent();
        }
        [Authorize]
        [HttpPost("{id:int}/user-take-part")]
        public async Task<IActionResult> UserTakePart(int id)
        {
            string? userId = User.GetUserId();
            if (string.IsNullOrWhiteSpace(userId)) return BadRequest("UserId is invalid");
            await _takingPartService.UserTakePartAsync(id, userId);
            return NoContent();
        }
        [HttpPost("{id:int}/guest-take-part")]
        public async Task<IActionResult> GuestTakePart(int id , [FromBody] AddGuestTakinPartDto addGuestTakinPartDto)
        {
            Validator.ValidateAndThrow(addGuestTakinPartDto, new AddGuestTakingPartValidator());
            await _takingPartService.GuestTakePartAsync(id, addGuestTakinPartDto);
            return NoContent();
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("{id:int}/user-participants")]
        public async Task<IActionResult> GetAllUserParticipantsByEventId(int id , [FromQuery] ParticipantFilteringParams participantFilteringParams)
        {
            List<ParticipantDto> participantDtos = await _takingPartService.GetAllUserParticipantsAsync(id , participantFilteringParams);
            return Ok(participantDtos);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("{id:int}/guest-participants")]
        public async Task<IActionResult> GetAllGuestParticipantsByEventId(int id , [FromQuery] ParticipantFilteringParams participantFilteringParams)
        {
            List<ParticipantDto> participantDtos = await _takingPartService.GetAllGuestParticipantsAsync(id , participantFilteringParams);
            return Ok(participantDtos);
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("{id:int}/taking-part")]
        public async Task<IActionResult> UpdateTakingPart(int id , [FromBody] bool isConfirmed)
        {
            await _takingPartService.UpdateTakingPartAsync(id, isConfirmed);
            return NoContent();
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("deny-taking-part/{takingPartId:int}")]
        public async Task<IActionResult> DenyTakingPart(int takingPartId)
        {
            await _takingPartService.DenyTakingPartAsync(takingPartId);
            return NoContent();
        }
        [HttpGet("{id:int}/suggestion")]
        public async Task<IActionResult> GetSuggestions(int id, [FromQuery] PaginationParams paginationParams)
        {
            List<EventSummaryDto> eventSummaryDtos = await _eventService.GetSuggestionsAsync(paginationParams, id);
            return Ok(eventSummaryDtos);
        }
    }
}