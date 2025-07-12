using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Weblog.Application.CustomExceptions;
using Weblog.Application.Dtos.EventDtos;
using Weblog.Application.Interfaces.Repositories;
using Weblog.Application.Interfaces.Services;
using Weblog.Domain.JoinModels;
using Weblog.Domain.Models;

namespace Weblog.Infrastructure.Services
{
    public class FavoriteEventService : IFavoriteEventService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IFavoriteEventRepository _favoriteEventRepo;
        private readonly IEventRepository _eventRepo;
        private readonly IMapper _mapper;

        public FavoriteEventService(IEventRepository eventRepo, IMapper mapper, UserManager<AppUser> userManager, IFavoriteEventRepository favoriteEventRepo)
        {
            _userManager = userManager;
            _favoriteEventRepo = favoriteEventRepo;
            _mapper = mapper;
            _eventRepo = eventRepo;
        }

        public async Task AddEventToFavoriteAsync(int eventId, string userId , int favoriteListId)
        {
            AppUser appUser = await _userManager.FindByIdAsync(userId) ?? throw new NotFoundException("User not found");
            Event eventModel = await _eventRepo.GetEventByIdAsync(eventId) ?? throw new NotFoundException("Event not found");
            bool eventAdded = await _favoriteEventRepo.EventAddedToFavoriteAsync(new FavoriteEvent { EventId = eventId, UserId = userId });
            if(eventAdded == true)
            {
                throw new ValidationException("The event already added into favorites");
            }
            FavoriteEvent favoriteEvent = new FavoriteEvent
            {
                UserId = appUser.Id,
                AppUser = appUser,
                EventId = eventId,
                Event = eventModel
            };
            await _favoriteEventRepo.AddEventToFavoriteAsync(favoriteEvent);
        }

        public async Task DeleteEventFromFavoriteAsync(int eventArticleId, string userId)
        {
            AppUser appUser = await _userManager.FindByIdAsync(userId) ?? throw new NotFoundException("User not found");
            FavoriteEvent favoriteEvent = await _favoriteEventRepo.GetFavoriteEventByIdAsync(eventArticleId) ?? throw new NotFoundException("Favorite event not found");
            if (appUser.Id != favoriteEvent.UserId)
            {
                throw new ValidationException("Favorite event not found");
            }

            await _favoriteEventRepo.DeleteEventFromFavoriteAsync(favoriteEvent);
        }

        public async Task<List<EventDto>> GetAllFavoriteEventsAsync(string userId)
        {
            AppUser appUser = await _userManager.FindByIdAsync(userId) ?? throw new NotFoundException("User not found");
            List<FavoriteEvent> favoriteEvents = await _favoriteEventRepo.GetAllFavoriteEventsAsync(userId);
            List<EventDto> eventDtos = _mapper.Map<List<EventDto>>(favoriteEvents.Select(f => f.Event));
            return eventDtos;
        }
    }
}