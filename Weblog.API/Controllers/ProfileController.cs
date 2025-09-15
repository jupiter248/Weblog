using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Weblog.Application.Dtos.UserProfileDtos;
using Weblog.Application.Extensions;
using Weblog.Application.Interfaces.Services;

namespace Weblog.API.Controllers
{
    [ApiController]
    [Route("api/user-profile")]
    public class ProfileController : ControllerBase
    {
        private readonly IUserProfileService _userProfileService;
        public ProfileController(IUserProfileService userProfileService)
        {
            _userProfileService = userProfileService;
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllProfiles()
        {
            List<UserProfileDto> userProfileDtos = await _userProfileService.GetAllProfilesAsync();
            return Ok(
                userProfileDtos
            );
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("{userProfileId:int}")]
        public async Task<IActionResult> GetUserProfileById(int userProfileId)
        {
            UserProfileDto userProfile = await _userProfileService.GetUserProfileByIdAsync(userProfileId);
            return Ok(
                userProfile
            );
        }
        [Authorize]
        [HttpPost]  
        public async Task<IActionResult> AddUserProfile([FromForm] UploadUserProfileDto uploadUserProfileDto)
        {
            string? userId = User.GetUserId();
            if (userId == null) return NotFound("User not found");
            UserProfileDto userProfile = await _userProfileService.AddUserProfileAsync(uploadUserProfileDto, userId);
            return CreatedAtAction(nameof(GetUserProfileById), new { userProfileId = userProfile.Id }, userProfile);
        } 
        [Authorize]
        [HttpDelete("{userProfileId:int}")]
        public async Task<IActionResult> DeleteUserProfile(int userProfileId)
        {
            string? userId = User.GetUserId();
            if (userId == null) return NotFound("User not found");
            await _userProfileService.DeleteUserProfileAsync(userProfileId ,userId);
            return NoContent();
        } 
    }
}