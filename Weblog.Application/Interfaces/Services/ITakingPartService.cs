using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblog.Application.Dtos.EventDtos;
using Weblog.Application.Queries.FilteringParams;
using Weblog.Domain.Models;

namespace Weblog.Application.Interfaces.Services
{
    public interface ITakingPartService
    {
        Task<List<ParticipantDto>> GetAllUserParticipantsAsync(int eventId , ParticipantFilteringParams participantFilteringParams);
        Task<List<ParticipantDto>> GetAllGuestParticipantsAsync(int eventId , ParticipantFilteringParams participantFilteringParams);
        Task<List<EventSummaryDto>> GetAllTookPartEventsAsync(string userId , int? categoryId);
        Task<bool> IsUserParticipantAsync(string userId, int eventId);
        Task UserTakePartAsync(int eventId , string userId );
        Task GuestTakePartAsync(int eventId , AddGuestTakinPartDto? addGuestTakinPartDto);
        Task DenyTakingPartAsync(int takingPartId);
        Task UpdateTakingPartAsync(int id , bool isConfirmed);

    }
}