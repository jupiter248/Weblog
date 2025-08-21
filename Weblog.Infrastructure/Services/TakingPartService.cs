using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Weblog.Application.CustomExceptions;
using Weblog.Application.Dtos.EventDtos;
using Weblog.Application.Interfaces.Repositories;
using Weblog.Application.Interfaces.Services;
using Weblog.Application.Queries.FilteringParams;
using Weblog.Domain.Errors.Event;
using Weblog.Domain.Errors.Participant;
using Weblog.Domain.Errors.User;
using Weblog.Domain.JoinModels;
using Weblog.Domain.Models;

namespace Weblog.Infrastructure.Services
{
    public class TakingPartService : ITakingPartService
    {
        private readonly ITakingPartRepository _takingPartRepo;
        private readonly IEventRepository _eventRepo;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;



        public TakingPartService(IMapper mapper, ITakingPartRepository takingPartRepo, IEventRepository eventRepo, UserManager<AppUser> userManager)
        {
            _takingPartRepo = takingPartRepo;
            _userManager = userManager;
            _eventRepo = eventRepo;
            _mapper = mapper;
        }
        public async Task CancelTakingPartAsync(int eventId, string userId)
        {
            AppUser appUser = await _userManager.FindByIdAsync(userId) ?? throw new NotFoundException(UserErrorCodes.UserNotFound);
            Event eventModel = await _eventRepo.GetEventByIdAsync(eventId) ?? throw new NotFoundException(EventErrorCodes.EventNotFound);

            TakingPart takingPart = await _takingPartRepo.GetTakingPartByUserIdAndEventIdAsync(userId, eventId) ?? throw new NotFoundException(ParticipantErrorCodes.ParticipantNotFound);
            await _takingPartRepo.CancelTakingPartAsync(takingPart);
        }

        public async Task UpdateTakingPartAsync(int id, bool isConfirmed)
        {
            TakingPart takingPart = await _takingPartRepo.GetTakingPartByIdAsync(id) ?? throw new NotFoundException(ParticipantErrorCodes.ParticipantNotFound);
            takingPart.IsConfirmed = isConfirmed;
            await _takingPartRepo.UpdateTakingPartAsync(takingPart);
        }

        public async Task<List<ParticipantDto>> GetAllParticipantsAsync(int eventId , ParticipantFilteringParams participantFilteringParams)
        {
            List<TakingPart> takingParts = await _takingPartRepo.GetAllTakingPartsByEventIdAsync(eventId , participantFilteringParams);
            List<ParticipantDto> participantDtos = takingParts.Select(s => new ParticipantDto()
            {
                Id = s.Id,
                FirstName = s.AppUser.FirstName,
                LastName = s.AppUser.LastName,
                Phone = s.AppUser.PhoneNumber,
                Username = s.AppUser.UserName,
                IsConfirmed = s.IsConfirmed

            }).ToList();
            return participantDtos;
        }

        public async Task TakePartAsync(int eventId, string userId)
        {
            AppUser appUser = await _userManager.FindByIdAsync(userId) ?? throw new NotFoundException(UserErrorCodes.UserNotFound);
            Event eventModel = await _eventRepo.GetEventByIdAsync(eventId) ?? throw new NotFoundException(EventErrorCodes.EventNotFound);
            TakingPart takingPart = new TakingPart
            {
                UserId = appUser.Id,
                AppUser = appUser,
                EventId = eventModel.Id,
                Event = eventModel
            };
            bool IsUserParticipant = await _takingPartRepo.IsUserParticipant(takingPart);
            if (IsUserParticipant)
            {
                throw new ConflictException("You already took part");
            }
            await _takingPartRepo.TakePartAsync(takingPart);
        }

        public async Task<List<EventSummaryDto>> GetAllTookPartEventsAsync(string userId , int? categoryId)
        {
            List<TakingPart> takingParts = await _takingPartRepo.GetAllTookPartsByEventsAsync(userId , categoryId);
            List<EventSummaryDto> eventSummaryDtos = _mapper.Map<List<EventSummaryDto>>(takingParts.Select(t => t.Event).ToList());
            if (categoryId.HasValue)
            {
                eventSummaryDtos = _mapper.Map<List<EventSummaryDto>>(eventSummaryDtos);
            }
            return eventSummaryDtos;
        }
    }
}