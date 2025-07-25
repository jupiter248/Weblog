using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblog.Application.Dtos.UserDtos;

namespace Weblog.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<List<UserDto>> GetAllUsersAsync();
        Task<UserDto> GetUserByIdAsync(string userId);
        Task UpdateUserAsync(UpdateUserDto updateUserDto , string userId);
        Task DeleteUserAsync(string userId);
    }
}