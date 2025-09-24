using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Weblog.Application.Dtos.EventDtos;
using Weblog.Application.Queries.FilteringParams;
using Weblog.Domain.JoinModels;

namespace Weblog.Application.Interfaces.Repositories
{
    public interface ITakingPartRepository
    {
        Task UserTakePartAsync(UserTakingPart takingPart);
        Task GuestTakePartAsync(GuestTakingPart takingPart);
        Task DenyTakingPartAsync(TakingPart takingPart);
        Task<List<UserTakingPart>> GetAllUserTakingPartsByEventIdAsync(int eventId , ParticipantFilteringParams participantFilteringParams);
        Task<List<GuestTakingPart>> GetAllGuestTakingPartsByEventIdAsync(int eventId , ParticipantFilteringParams participantFilteringParams);
        Task<List<UserTakingPart>> GetAllTookPartsByEventsAsync(string userId , int? categoryId);
        Task<TakingPart?> GetTakingPartByIdAsync(int id);
        Task<UserTakingPart?> GetTakingPartByUserIdAndEventIdAsync(string userId , int eventId);
        Task<bool> IsUserParticipantAsync(UserTakingPart takingPart);
        Task UpdateTakingPartAsync(TakingPart takingPart);
    }
}