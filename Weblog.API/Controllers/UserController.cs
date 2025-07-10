using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Weblog.Application.Dtos.UserDtos;
using Weblog.Application.Interfaces.Services;

namespace Weblog.API.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            List<UserDto> userDtos = await _userService.GetAllUsersAsync();
            return Ok(userDtos);
        }
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserById(string userId)
        {
            UserDto userDto = await _userService.GetUserByIdAsync(userId);
            return Ok(userDto);
        }
        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUser(string userId , UpdateUserDto updateUserDto)
        {
            await _userService.UpdateUserAsync(updateUserDto , userId);
            return Ok("User updated successfully");
        } 
        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            await _userService.DeleteUserAsync(userId);
            return Ok("User deleted successfully");
        }
    }
}