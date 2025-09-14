using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblog.Domain.Models;

namespace Weblog.Application.Interfaces.Repositories
{
    public interface IUserProfileRepository
    {
        public Task<List<UserProfile>> GetAllProfilesAsync();
        public Task<UserProfile?> GetUserProfileByIdAsync(int userProfileId);
        public Task AddUserProfileAsync(UserProfile userProfile);
        public Task DeleteUserProfileAsync(UserProfile userProfile);
    }
}