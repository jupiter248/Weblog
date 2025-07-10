using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Weblog.Application.CustomExceptions;
using Weblog.Application.Dtos.EventDtos;
using Weblog.Application.Interfaces.Repositories;
using Weblog.Application.Interfaces.Services;
using Weblog.Domain.JoinModels.Favorites;
using Weblog.Domain.Models;

namespace Weblog.Infrastructure.Services
{
    public class TakingPartService : ITakingPartService
    {
        private readonly ITakingPartRepository _takingPartRepo;
        private readonly IEventRepository _eventRepo;
        private readonly UserManager<AppUser> _userManager;


        public TakingPartService(ITakingPartRepository takingPartRepo, IEventRepository eventRepo, UserManager<AppUser> userManager)
        {
            _takingPartRepo = takingPartRepo;
            _userManager = userManager;
            _eventRepo = eventRepo;
        }
        public async Task CancelTakingPartAsync(int eventId, string userId)
        {
            AppUser appUser = await _userManager.FindByIdAsync(userId) ?? throw new NotFoundException("User not found");
            Event eventModel = await _eventRepo.GetEventByIdAsync(eventId) ?? throw new NotFoundException("Event not found");
            TakingPart takingPart = new TakingPart
            {
                UserId = appUser.Id,
                AppUser = appUser,
                EventId = eventModel.Id,
                Event = eventModel
            };
            await _takingPartRepo.CancelTakingPartAsync(takingPart);
        }

        public async Task UpdateTakingPartAsync(int id, bool isConfirmed)
        {
            TakingPart takingPart = await _takingPartRepo.GetTakingPartByIdAsync(id) ?? throw new NotFoundException("Taking part not found");
            takingPart.IsConfirmed = isConfirmed;
            await _takingPartRepo.UpdateTakingPartAsync(takingPart);
        }

        public async Task<List<ParticipantDto>> GetAllParticipantsAsync(int eventId)
        {
            List<TakingPart> takingParts = await _takingPartRepo.GetAllTakingPartsByEventIdAsync(eventId);
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
            AppUser appUser = await _userManager.FindByIdAsync(userId) ?? throw new NotFoundException("User not found");
            Event eventModel = await _eventRepo.GetEventByIdAsync(eventId) ?? throw new NotFoundException("Event not found");
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
    }
}