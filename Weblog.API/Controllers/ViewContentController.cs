using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Weblog.Application.Extensions;
using Weblog.Application.Interfaces.Services;
using Weblog.Domain.Enums;

namespace Weblog.API.Controllers
{
    [ApiController]
    [Route("api/content/view")]
    public class ViewContentController : ControllerBase
    {
        private readonly IViewContentService _viewContentService;
        public ViewContentController(IViewContentService viewContentService)
        {
            _viewContentService = viewContentService;
        }
        [HttpGet]
        public async Task<IActionResult> GetViewCount(int entityTypeId, LikeAndViewType entityType)
        {
            int viewCount = await _viewContentService.GetViewCountAsync(entityTypeId, entityType);
            return Ok(viewCount);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ViewContent(int entityTypeId, LikeAndViewType entityType)
        {
            string? userId = User.GetUserId();
            if (userId == null) return NotFound("User not found");
            await _viewContentService.AddViewContentAsync(userId, entityTypeId, entityType);
            return NoContent();
        }
    }
}