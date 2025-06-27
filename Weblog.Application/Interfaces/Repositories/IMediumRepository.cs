using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblog.Domain.Models;

namespace Weblog.Application.Interfaces.Repositories
{
    public interface IMediumRepository
    {
        public Task<List<Medium>> GetAllMediaAsync();
        public Task<Medium?> GetMediumByIdAsync(int mediumId);
        public Task<Medium> AddMediumAsync(Medium medium);
        public Task UpdateMediumAsync(Medium medium);
        public Task DeleteMediumAsync(Medium medium);
    }
}