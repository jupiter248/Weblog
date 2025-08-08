using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Weblog.Application.Dtos.TagDtos;
using Weblog.Application.Interfaces.Services;
using Weblog.Infrastructure.Services;

namespace Weblog.API.Controllers
{
    [ApiController]
    [Route("api/tag")]
    public class TagController : ControllerBase
    {
        private readonly ITagService _tagService;
        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllTags()
        {
            List<TagDto> tagDtos = await _tagService.GetAllTagsAsync();
            return Ok(tagDtos);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetTagById( int id)
        {
            TagDto tagDto = await _tagService.GetTagByIdAsync(id);
            return Ok(tagDto);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddTag([FromBody] AddTagDto addTagDto)
        {
            TagDto tagDto = await _tagService.AddTagAsync(addTagDto);
            return CreatedAtAction(nameof(GetTagById), new { id = tagDto.Id }, tagDto);
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateTag([FromBody] UpdateTagDto updateTagDto, [FromRoute] int id)
        {
            await _tagService.UpdateTagAsync(updateTagDto, id);
            return NoContent();
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteTag( int id)
        {
            await _tagService.DeleteTagAsync( id);
            return NoContent();
        }
    }
}