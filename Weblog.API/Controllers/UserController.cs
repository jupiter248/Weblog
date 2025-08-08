using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Weblog.Application.Dtos.EventDtos;
using Weblog.Application.Dtos.UserDtos;
using Weblog.Application.Extensions;
using Weblog.Application.Interfaces.Services;

namespace Weblog.API.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITakingPartService _takingPartService;
        public UserController(IUserService userService, ITakingPartService takingPartService)
        {
            _userService = userService;
            _takingPartService = takingPartService;
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            List<UserDto> userDtos = await _userService.GetAllUsersAsync();
            return Ok(userDtos);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserById(string userId)
        {
            UserDto userDto = await _userService.GetUserByIdAsync(userId);
            return Ok(userDto);
        }
        [Authorize]
        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUser(string userId,[FromBody] UpdateUserDto updateUserDto)
        {
            await _userService.UpdateUserAsync(updateUserDto, userId);
            return Ok("User updated successfully");
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            await _userService.DeleteUserAsync(userId);
            return Ok("User deleted successfully");
        }
        [HttpGet("events")]
        public async Task<IActionResult> GetAllUserEvents([FromQuery] int? categoryId)
        {
            string? userId = User.GetUserId();
            if (userId == null) return NotFound("User not found");
            List<EventSummaryDto> eventSummaryDtos = await _takingPartService.GetAllTookPartEventsAsync(userId , categoryId);
            return Ok(eventSummaryDtos);
        }

    }
}