using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Weblog.Application.Dtos.MediaDtos;
using Weblog.Application.Extensions;
using Weblog.Application.Interfaces.Services;

namespace Weblog.API.Controllers
{
    [ApiController]
    [Route("api/medium")]
    public class MediumController : ControllerBase
    {
        private readonly IMediumService _mediumService;
        public MediumController(IMediumService mediumService)
        {
            _mediumService = mediumService;
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllMedia()
        {
            List<MediumDto> mediumDtos = await _mediumService.GetAllMediaAsync();
            return Ok(mediumDtos);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetMediumById(int id)
        {
            MediumDto mediumDto = await _mediumService.GetMediumByIdAsync(id);
            return Ok(mediumDto);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddMedium(UploadMediumDto uploadMediumDto)
        {
            string? userId = User.GetUserId();
            if (userId == null) return NotFound("User not found");
            MediumDto mediumDto = await _mediumService.StoreMediumAsync(uploadMediumDto , userId);
            return CreatedAtAction(nameof(GetMediumById), new { id = mediumDto.Id }, mediumDto);
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateMedium(UpdateMediumDto updateMediumDto, int id)
        {
            await _mediumService.UpdateMediumAsync(updateMediumDto, id);
            return NoContent();
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteMedium(int id)
        {
            string? userId = User.GetUserId();
            if (userId == null) return NotFound("User not found");
            await _mediumService.DeleteMediumAsync(id , userId);
            return NoContent();
        }
    }
}