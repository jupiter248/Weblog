using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblog.Domain.JoinModels.Favorites;

namespace Weblog.Application.Interfaces.Repositories
{
    public interface ITakingPartRepository
    {
        Task TakePartAsync(TakingPart takingPart);
        Task CancelTakingPartAsync(TakingPart takingPart);
        Task<List<TakingPart>> GetAllTakingPartsAsync(string userId);
        Task<TakingPart?> GetTakingPartByIdAsync(int id);
        Task<bool> IsUserParticipant(TakingPart takingPart);



    }
}