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
        Task<List<ParticipantDto>> GetAllParticipantsAsync(int eventId , ParticipantFilteringParams participantFilteringParams);
        Task<List<EventSummaryDto>> GetAllTookPartEventsAsync(string userId , int? categoryId);
        Task<bool> IsUserParticipantAsync(string userId, int eventId);
        Task TakePartAsync(int eventId , string userId);
        Task CancelTakingPartAsync(int eventId , string userId);
        Task UpdateTakingPartAsync(int id , bool isConfirmed);

    }
}