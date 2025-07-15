using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Weblog.Application.Dtos.ArticleDtos;
using Weblog.Application.Dtos.EventDtos;
using Weblog.Application.Dtos.FavoriteListDtos;
using Weblog.Application.Dtos.PodcastDtos;
using Weblog.Application.Extensions;
using Weblog.Application.Interfaces.Services;
using Weblog.Domain.JoinModels;

namespace Weblog.API.Controllers
{
    [ApiController]
    [Route("api/favorite-list")]
    public class FavoriteListController : ControllerBase
    {
        private readonly IFavoriteListService _favoriteListService;
        private readonly IFavoriteArticleService _favoriteArticleService;
        private readonly IFavoriteEventService _favoriteEventService;
        private readonly IFavoritePodcastService _favoritePodcastService;
        public FavoriteListController(IFavoriteListService favoriteListService, IFavoriteArticleService favoriteArticleService, IFavoriteEventService favoriteEventService, IFavoritePodcastService favoritePodcastService)
        {
            _favoriteListService = favoriteListService;
            _favoriteArticleService = favoriteArticleService;
            _favoriteEventService = favoriteEventService;
            _favoritePodcastService = favoritePodcastService;
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllFavoriteLists()
        {
            string? userId = User.GetUserId();
            if (userId == null) return NotFound("User not found");
            List<FavoriteListDto> favoriteListDtos = await _favoriteListService.GetAllFavoriteListsAsync(userId);
            return Ok(favoriteListDtos);
        }
        [Authorize]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetFavoriteListById([FromRoute] int id)
        {
            FavoriteListDto favoriteListDto = await _favoriteListService.GetFavoriteListByIdAsync(id);
            return Ok(favoriteListDto);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddFavoriteList([FromQuery] AddFavoriteListDto addFavoriteListDto)
        {
            string? userId = User.GetUserId();
            if (userId == null) return NotFound("User not found");
            FavoriteListDto favoriteListDto = await _favoriteListService.AddFavoriteListAsync(userId, addFavoriteListDto);
            return CreatedAtAction(nameof(GetFavoriteListById), new { id = favoriteListDto.Id }, favoriteListDto);
        }
        [Authorize]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateFavoriteList([FromRoute] int id, [FromBody] UpdateFavoriteListDto updateFavoriteListDto)
        {
            string? userId = User.GetUserId();
            if (userId == null) return NotFound("User not found");
            await _favoriteListService.UpdateFavoriteListAsync(userId, updateFavoriteListDto, id);
            return NoContent();
        }
        [Authorize]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteFavoriteList([FromRoute] int id)
        {
            string? userId = User.GetUserId();
            if (userId == null) return NotFound("User not found");
            await _favoriteListService.DeleteFavoriteList(userId, id);
            return NoContent();
        }
        [Authorize]
        [HttpGet("event")]
        public async Task<IActionResult> GetAllFavoriteEvents([FromQuery] int? favoriteListId)
        {
            string? userId = User.GetUserId();
            if (userId == null) return NotFound("User not found");
            List<EventDto> eventDtos = await _favoriteEventService.GetAllFavoriteEventsAsync(userId , favoriteListId);
            return Ok(eventDtos);
        }
        [Authorize]
        [HttpGet("podcast")]
        public async Task<IActionResult> GetAllFavoritePodcasts([FromQuery] int? favoriteListId)
        {
            string? userId = User.GetUserId();
            if (userId == null) return NotFound("User not found");
            List<PodcastDto> podcastDtos = await _favoritePodcastService.GetAllFavoritePodcastsAsync(userId , favoriteListId);
            return Ok(podcastDtos);
        }
        [Authorize]
        [HttpGet("article")]
        public async Task<IActionResult> GetAllFavoriteArticles([FromQuery] int? favoriteListId)
        {
            string? userId = User.GetUserId();
            if (userId == null) return NotFound("User not found");
            List<ArticleDto> articleDtos = await _favoriteArticleService.GetAllFavoriteArticlesAsync(userId ,favoriteListId);
            return Ok(articleDtos);
        }
    }
}