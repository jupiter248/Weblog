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
        Task TakePartAsync(TakingPart takingPart);
        Task CancelTakingPartAsync(TakingPart takingPart);
        Task<List<TakingPart>> GetAllTakingPartsByEventIdAsync(int eventId , ParticipantFilteringParams participantFilteringParams);
        Task<List<TakingPart>> GetAllTookPartsByEventsAsync(string userId , int? categoryId);
        Task<TakingPart?> GetTakingPartByIdAsync(int id);
        Task<TakingPart?> GetTakingPartByUserIdAndEventIdAsync(string userId , int eventId);
        Task<bool> IsUserParticipant(TakingPart takingPart);
        Task UpdateTakingPartAsync(TakingPart takingPart);
    }
}