using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Weblog.Application.CustomExceptions;
using Weblog.Application.Dtos.EventDtos;
using Weblog.Application.Dtos.FavoritesDtos.EventFavoriteDtos;
using Weblog.Application.Interfaces.Repositories;
using Weblog.Application.Interfaces.Services;
using Weblog.Domain.Errors.Event;
using Weblog.Domain.Errors.Favorite;
using Weblog.Domain.Errors.User;
using Weblog.Domain.JoinModels;
using Weblog.Domain.JoinModels.Favorites;
using Weblog.Domain.Models;

namespace Weblog.Infrastructure.Services
{
    public class FavoriteEventService : IFavoriteEventService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IFavoriteEventRepository _favoriteEventRepo;
        private readonly IEventRepository _eventRepo;
        private readonly IFavoriteListRepository _favoriteListRepo;

        private readonly IMapper _mapper;

        public FavoriteEventService(IFavoriteListRepository favoriteListRepository, IEventRepository eventRepo, IMapper mapper, UserManager<AppUser> userManager, IFavoriteEventRepository favoriteEventRepo)
        {
            _userManager = userManager;
            _favoriteEventRepo = favoriteEventRepo;
            _mapper = mapper;
            _eventRepo = eventRepo;
            _favoriteListRepo = favoriteListRepository;
        }

        public async Task AddEventToFavoriteAsync(string userId,AddFavoriteEventDto addFavoriteEventDto)
        {
            AppUser appUser = await _userManager.FindByIdAsync(userId) ?? throw new NotFoundException(UserErrorCodes.UserNotFound);
            Event eventModel = await _eventRepo.GetEventByIdAsync(addFavoriteEventDto.EventId) ?? throw new NotFoundException(EventErrorCodes.EventNotFound);
            if (addFavoriteEventDto.favoriteListId.HasValue)
            {
                FavoriteList favoriteList = await _favoriteListRepo.GetFavoriteListByIdAsync(addFavoriteEventDto.favoriteListId) ?? throw new NotFoundException(FavoriteErrorCodes.FavoriteListNotFound);   
            }
            bool eventAdded = await _favoriteEventRepo.EventAddedToFavoriteAsync(new FavoriteEvent { EventId = addFavoriteEventDto.EventId, UserId = userId });
            if(eventAdded == true)
            {
                throw new ConflictException(FavoriteErrorCodes.FavoriteItemAlreadyExists);
            }
            FavoriteEvent favoriteEvent = new FavoriteEvent
            {
                UserId = appUser.Id,
                AppUser = appUser,
                EventId = eventModel.Id,
                Event = eventModel,
                FavoriteListId = addFavoriteEventDto.favoriteListId
            };
            await _favoriteEventRepo.AddEventToFavoriteAsync(favoriteEvent);
        }

        public async Task DeleteEventFromFavoriteAsync(int eventArticleId, string userId)
        {
            AppUser appUser = await _userManager.FindByIdAsync(userId) ?? throw new NotFoundException(UserErrorCodes.UserNotFound);
            FavoriteEvent favoriteEvent = await _favoriteEventRepo.GetFavoriteEventByIdAsync(eventArticleId) ?? throw new NotFoundException(FavoriteErrorCodes.FavoriteItemNotFound);
            if (appUser.Id != favoriteEvent.UserId)
            {
                throw new NotFoundException(FavoriteErrorCodes.FavoriteItemNotFound);
            }

            await _favoriteEventRepo.DeleteEventFromFavoriteAsync(favoriteEvent);
        }

        public async Task<List<EventDto>> GetAllFavoriteEventsAsync(string userId , int? favoriteListId)
        {
            AppUser appUser = await _userManager.FindByIdAsync(userId) ?? throw new NotFoundException(UserErrorCodes.UserNotFound);
            List<FavoriteEvent> favoriteEvents = await _favoriteEventRepo.GetAllFavoriteEventsAsync(userId);
            if (favoriteListId.HasValue)
            {
               favoriteEvents = favoriteEvents.Where(f => f.FavoriteListId == favoriteListId).ToList();
            }
            List<EventDto> eventDtos = _mapper.Map<List<EventDto>>(favoriteEvents.Select(f => f.Event));
            return eventDtos;
        }
    }
}