using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblog.Application.Dtos.UserProfileDtos;
using Weblog.Domain.Models;

namespace Weblog.Application.Interfaces.Services
{
    public interface IUserProfileService
    {
        public Task<List<UserProfileDto>> GetAllProfilesAsync();
        public Task<UserProfileDto> GetUserProfileByIdAsync(int userProfileId);
        public Task<UserProfileDto> AddUserProfileAsync(UploadUserProfileDto uploadUserProfileDto , string userId);
        public Task DeleteUserProfileAsync(int userProfileId , string userId);
    }
}