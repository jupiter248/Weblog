using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Weblog.Application.Dtos.LikeContentDtos;
using Weblog.Application.Dtos.UserDtos;
using Weblog.Application.Extensions;
using Weblog.Application.Interfaces.Services;
using Weblog.Domain.Enums;

namespace Weblog.API.Controllers
{
    [ApiController]
    [Route("api/content/like")]
    public class LikeContentController : ControllerBase
    {
        private readonly ILikeContentService _likeContentService;
        public LikeContentController(ILikeContentService likeContentService)
        {
            _likeContentService = likeContentService;
        }
        [HttpGet("users")]
        public async Task<IActionResult> GetAllLikeUsers(int entityTypeId,[FromQuery] LikeAndViewType likeAndViewType)
        {
            List<UserDto> userDtos = await _likeContentService.GetAllContentLikesAsync(entityTypeId, likeAndViewType);
            return Ok(userDtos);
        }
        [HttpGet("count")]
        public async Task<IActionResult> GetLikeCount(int entityTypeId,[FromQuery] LikeAndViewType entityType)
        {
            int likeCount = await _likeContentService.GetLikeCountAsync(entityTypeId, entityType);
            return Ok(likeCount);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> LikeContent([FromQuery] LikeContentDto likeContentDto)
        {
            string? userId = User.GetUserId();
            if (userId == null) return NotFound("User not found");
            await _likeContentService.LikeAsync(userId, likeContentDto);
            return NoContent();
        }
        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> UnlikeContent([FromQuery] UnLikeContentDto unLikeContentDto)
        {
            string? userId = User.GetUserId();
            if (userId == null) return NotFound("User not found");
            await _likeContentService.UnlikeAsync(userId, unLikeContentDto);
            return NoContent();
        }
    }
}