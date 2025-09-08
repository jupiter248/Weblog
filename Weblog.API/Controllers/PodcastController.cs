using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Weblog.Application.Dtos.FavoritesDtos.EventFavoriteDtos;
using Weblog.Application.Dtos.FavoritesDtos.PodcastFavoriteDto;
using Weblog.Application.Dtos.PodcastDtos;
using Weblog.Application.Extensions;
using Weblog.Application.Interfaces.Repositories;
using Weblog.Application.Interfaces.Services;
using Weblog.Application.Queries;
using Weblog.Application.Queries.FilteringParams;
using Weblog.Application.Validations;
using Weblog.Application.Validations.Podcast;

namespace Weblog.API.Controllers
{
    [ApiController]
    [Route("api/podcast")]
    public class PodcastController : ControllerBase
    {
        private readonly IPodcastService _podcastService;
        private readonly IFavoritePodcastService _favoritePodcastService;

        public PodcastController(IPodcastService podcastService, IFavoritePodcastService favoritePodcastService)
        {
            _podcastService = podcastService;
            _favoritePodcastService = favoritePodcastService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllPodcasts([FromQuery] PodcastFilteringParams podcastFilteringParams, [FromQuery] PaginationParams paginationParams)
        {
            List<PodcastSummaryDto> podcastSummaryDtos = await _podcastService.GetAllPodcastsAsync(paginationParams, podcastFilteringParams);
            return Ok(podcastSummaryDtos);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetPodcastById(int id)
        {
            PodcastDto podcastDto = await _podcastService.GetPodcastByIdAsync(id);
            return Ok(podcastDto);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddPodcast([FromBody] AddPodcastDto addPodcastDto)
        {
            Validator.ValidateAndThrow(addPodcastDto, new AddPodcastValidator());
            PodcastDto podcastDto = await _podcastService.AddPodcastAsync(addPodcastDto);
            return CreatedAtAction(nameof(GetPodcastById), new { id = podcastDto.Id }, podcastDto);
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdatePodcast(int id,[FromBody] UpdatePodcastDto updatePodcastDto)
        {
            Validator.ValidateAndThrow(updatePodcastDto, new UpdatePodcastValidator());
            PodcastDto podcastDto = await _podcastService.UpdatePodcastAsync(updatePodcastDto, id);
            return Ok(podcastDto);
        }
        [HttpPut("{podcastId:int}/view")]
        public async Task<IActionResult> IncrementPodcastView(int podcastId)
        {
            int podcastCount = await _podcastService.IncrementPodcastViewAsync(podcastId);
            return Ok(new
            {
                podcastCount = podcastCount
            });
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeletePodcast(int id)
        {
            await _podcastService.DeletePodcastAsync(id);
            return NoContent();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("{id:int}/tag")]
        public async Task<IActionResult> AddTagToPodcast(int id, int tagId)
        {
            await _podcastService.AddTagAsync(id, tagId);
            return NoContent();
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:int}/tag")]
        public async Task<IActionResult> DeleteTagOfPodcast(int id, int tagId)
        {
            await _podcastService.DeleteTagAsync(id, tagId);
            return NoContent();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("{id:int}/contributor")]
        public async Task<IActionResult> AddContributorToPodcast(int id, int contributorId)
        {
            await _podcastService.AddContributorAsync(id, contributorId);
            return NoContent();
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:int}/contributor")]
        public async Task<IActionResult> DeleteContributorOfPodcast(int id, int contributorId)
        {
            await _podcastService.DeleteContributorAsync(id, contributorId);
            return NoContent();
        }
        [Authorize]
        [HttpPost("favorite")]
        public async Task<IActionResult> AddArticleToFavorite([FromBody] AddFavoritePodcastDto addFavoritePodcastDto)
        {
            string? userId = User.GetUserId();
            if (string.IsNullOrWhiteSpace(userId)) return BadRequest("UserId is invalid");
            await _favoritePodcastService.AddPodcastToFavoriteAsync(userId, addFavoritePodcastDto);
            return NoContent();
        }
        [Authorize]
        [HttpDelete("{id:int}/favorite")]
        public async Task<IActionResult> DeleteArticleOfFavorite(int id)
        {
            string? userId = User.GetUserId();
            if (string.IsNullOrWhiteSpace(userId)) return BadRequest("UserId is invalid");
            await _favoritePodcastService.DeletePodcastFromFavoriteAsync(id, userId);
            return NoContent();
        }
        [HttpGet("{id:int}/suggestion")]
        public async Task<IActionResult> GetSuggestions(int id, [FromQuery] PaginationParams paginationParams)
        {
            List<PodcastSummaryDto> podcastSummaryDtos = await _podcastService.GetSuggestionsAsync(paginationParams, id);
            return Ok(podcastSummaryDtos);
        }
    }
}