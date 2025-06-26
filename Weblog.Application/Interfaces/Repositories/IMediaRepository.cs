using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblog.Domain.Models;

namespace Weblog.Application.Interfaces.Repositories
{
    public interface IMediaRepository
    {
        public Task<List<Medium>> GetAllMediumAsync();
        public Task<Medium?> GetMediaByIdAsync(int mediumId);
        public Task<Medium> AddMediaAsync(Medium medium);
        public Task UpdateMediaAsync(Medium medium);
        public Task DeleteMediaAsync(Medium medium);
    }
}