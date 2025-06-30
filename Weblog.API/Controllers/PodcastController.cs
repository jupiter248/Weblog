using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Weblog.Application.Dtos.PodcastDtos;
using Weblog.Application.Interfaces.Services;
using Weblog.Application.Queries;

namespace Weblog.API.Controllers
{
    [ApiController]
    [Route("api/podcast")]
    public class PodcastController : ControllerBase
    {
        private readonly IPodcastService _podcastService;
        public PodcastController(IPodcastService podcastService)
        {
            _podcastService = podcastService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllPodcasts([FromQuery] FilteringParams filteringParams, [FromQuery] PaginationParams paginationParams)
        {
            List<PodcastDto> podcastDtos = await _podcastService.GetAllPodcastsAsync(paginationParams, filteringParams);
            return Ok(podcastDtos);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetPodcastById(int id)
        {
            PodcastDto podcastDto = await _podcastService.GetPodcastByIdAsync(id);
            return Ok(podcastDto);
        }
        [HttpPost]
        public async Task<IActionResult> AddPodcast([FromBody] AddPodcastDto addPodcastDto)
        {
            PodcastDto podcastDto = await _podcastService.AddPodcastAsync(addPodcastDto);
            return CreatedAtAction(nameof(GetPodcastById), new { id = podcastDto.Id }, podcastDto);
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdatePodcast(int id, UpdatePodcastDto updatePodcastDto)
        {
            await _podcastService.UpdatePodcastAsync(updatePodcastDto, id);
            return NoContent();
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeletePodcast(int id)
        {
            await _podcastService.DeletePodcastAsync(id);
            return NoContent();
        }
        [HttpPost("{id:int}/tag")]
        public async Task<IActionResult> AddTagToPodcast(int id, int tagId)
        {
            await _podcastService.AddTagAsync(id, tagId);
            return NoContent();
        }
        [HttpDelete("{id:int}/tag")]
        public async Task<IActionResult> DeleteTagOfPodcast(int id, int tagId)
        {
            await _podcastService.DeleteTagAsync(id, tagId);
            return NoContent();
        }
        [HttpPost("{id:int}/contributor")]
        public async Task<IActionResult> AddContributorToPodcast(int id, int contributorId)
        {
            await _podcastService.AddContributorAsync(id, contributorId);
            return NoContent();
        }
        [HttpDelete("{id:int}/contributor")]
        public async Task<IActionResult> DeleteContributorOfPodcast(int id, int contributorId)
        {
            await _podcastService.DeleteContributorAsync(id , contributorId);
            return NoContent();
        }
    }
}