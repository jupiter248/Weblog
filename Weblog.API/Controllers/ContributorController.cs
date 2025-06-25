using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Weblog.Application.Dtos.ContributorDtos;
using Weblog.Application.Interfaces.Services;

namespace Weblog.API.Controllers
{
    [ApiController]
    [Route("api/contributor")]
    public class ContributorController : ControllerBase
    {
        private readonly IContributorService _contributorService;
        public ContributorController(IContributorService contributorService)
        {
            _contributorService = contributorService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllContributors()
        {
            List<ContributorDto> contributorDtos = await _contributorService.GetAllContributorsAsync();
            return Ok(contributorDtos);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetContributorById([FromRoute] int id)
        {
            ContributorDto contributorDto = await _contributorService.GetContributorByIdAsync(id);
            return Ok(contributorDto);
        }
        [HttpPost]
        public async Task<IActionResult> AddContributor([FromBody] AddContributorDto addContributorDto)
        {
            ContributorDto contributorDto = await _contributorService.AddContributorAsync(addContributorDto);
            return CreatedAtAction(nameof(GetContributorById), new { id = contributorDto.Id }, contributorDto);
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateContributor([FromRoute] int id, [FromBody] UpdateContributorDto updateContributorDto)
        {
            await _contributorService.UpdateContributorAsync(updateContributorDto, id);
            return NoContent();
        } 
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteContributor([FromRoute] int id)
        {
            await _contributorService.DeleteContributorAsync( id);
            return NoContent();
        } 
    }
}