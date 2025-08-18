using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Weblog.Application.CustomExceptions;
using Weblog.Application.Dtos.UserDtos;
using Weblog.Application.Extensions;
using Weblog.Application.Interfaces.Services;
using Weblog.Domain.Errors.User;
using Weblog.Domain.Models;

namespace Weblog.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public UserService(UserManager<AppUser> userManager, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task DeleteUserAsync(string userId)
        {
            AppUser appUser = await _userManager.FindByIdAsync(userId) ?? throw new NotFoundException(UserErrorCodes.UserNotFound);
            await _userManager.DeleteAsync(appUser);
        }

        public async Task<List<UserDto>> GetAllUsersAsync()
        {
            List<AppUser> appUsers = await _userManager.Users.ToListAsync();
            List<UserDto> userDtos = _mapper.Map<List<UserDto>>(appUsers);
            foreach (var user in appUsers)
            {
                var roles = await _userManager.GetRolesAsync(user);
                foreach (var userDto in userDtos)
                {
                    if (userDto.Id == user.Id)
                    {
                        userDto.Roles = roles;
                    }
                }
            }
            return userDtos;
        }

        public async Task<UserDto> GetCurrentUser(string userId)
        {
            AppUser? appUser = await _userManager.FindByIdAsync(userId) ?? throw new NotFoundException(UserErrorCodes.UserNotFound);
            UserDto userDto = _mapper.Map<UserDto>(appUser);
            userDto.Roles = await _userManager.GetRolesAsync(appUser);
            return userDto;
        }

        public async Task<UserDto> GetUserByIdAsync(string userId)
        {
            AppUser appUser = await _userManager.FindByIdAsync(userId) ?? throw new NotFoundException(UserErrorCodes.UserNotFound);
            UserDto userDto = _mapper.Map<UserDto>(appUser);
            userDto.Roles = await _userManager.GetRolesAsync(appUser);

            return userDto;
        }

        public async Task UpdateUserAsync(UpdateUserDto updateUserDto, string userId)
        {

            var currentUser = _httpContextAccessor.HttpContext?.User;
            string? currentUserId = currentUser?.GetUserId();

            if (currentUserId != userId && !currentUser.IsInRole("Admin"))
            {
                throw new NotFoundException("Not a correct user or an admin");
            }
            AppUser appUser = await _userManager.FindByIdAsync(userId) ?? throw new NotFoundException(UserErrorCodes.UserNotFound);

            appUser = _mapper.Map(updateUserDto, appUser);
            var result = await _userManager.ChangePasswordAsync(appUser , updateUserDto.OldPassword , updateUserDto.NewPassword );
            if (!result.Succeeded)
            {
                throw new UnauthorizedException(UserErrorCodes.PasswordChangeFailed , []);
            }
            appUser.UpdatedAt = DateTimeOffset.Now;
            appUser.FullName = $"{appUser.FirstName} {appUser.LastName}";
            

            await _userManager.UpdateAsync(appUser);
        }
    }
}