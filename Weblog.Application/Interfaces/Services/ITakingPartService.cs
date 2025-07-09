using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblog.Application.Dtos.EventDtos;
using Weblog.Domain.Models;

namespace Weblog.Application.Interfaces.Services
{
    public interface ITakingPartService
    {
        Task<List<ParticipantDto>> GetAllParticipantsAsync(int eventId);
        Task TakePartAsync(int eventId , string userId);
        Task CancelTakingPartAsync(int eventId , string userId);
        Task UpdateTakingPartAsync(int id , bool isConfirmed);

    }
}