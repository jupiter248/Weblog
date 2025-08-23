using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Weblog.Application.Dtos.ContributorDtos;
using Weblog.Application.Interfaces.Services;
using Weblog.Application.Validations;
using Weblog.Application.Validations.Contributor;

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
        public async Task<IActionResult> GetContributorById( int id)
        {
            ContributorDto contributorDto = await _contributorService.GetContributorByIdAsync(id);
            return Ok(contributorDto);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddContributor([FromBody] AddContributorDto addContributorDto)
        {
            Validator.ValidateAndThrow(addContributorDto, new AddContributorValidator());
            ContributorDto contributorDto = await _contributorService.AddContributorAsync(addContributorDto);
            return CreatedAtAction(nameof(GetContributorById), new { id = contributorDto.Id }, contributorDto);
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateContributor( int id, [FromBody] UpdateContributorDto updateContributorDto)
        {
            Validator.ValidateAndThrow(updateContributorDto, new UpdateContributorValidator());
            await _contributorService.UpdateContributorAsync(updateContributorDto, id);
            return NoContent();
        } 
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteContributor( int id)
        {
            await _contributorService.DeleteContributorAsync( id);
            return NoContent();
        } 
    }
}