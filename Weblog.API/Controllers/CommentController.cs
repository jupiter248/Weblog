using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Weblog.Application.Dtos.CommentDtos;
using Weblog.Application.Extensions;
using Weblog.Application.Interfaces.Services;
using Weblog.Application.Queries;
using Weblog.Application.Queries.FilteringParams;

namespace Weblog.API.Controllers
{
    [ApiController]
    [Route("api/comment")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;
        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllComments([FromQuery] CommentFilteringParams commentFilteringParams, [FromQuery] PaginationParams paginationParams)
        {
            List<CommentDto> commentDtos = await _commentService.GetAllCommentsAsync(commentFilteringParams, paginationParams);
            return Ok(commentDtos);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCommentById([FromRoute] int id)
        {
            CommentDto commentDtos = await _commentService.GetCommentByIdAsync(id);
            return Ok(commentDtos);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddComment([FromQuery] AddCommentDto addCommentDto)
        {
            string? userId = User.GetUserId();
            if (userId == null) return NotFound("User not found");
            CommentDto categoryDto = await _commentService.AddCommentAsync(addCommentDto,userId );
            return CreatedAtAction(nameof(GetCommentById), new { id = categoryDto.Id }, categoryDto);
        }
        [Authorize]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateComment([FromRoute] int id, [FromBody] UpdateCommentDto updateCommentDto)
        {
            string? userId = User.GetUserId();
            if (userId == null) return NotFound("User not found");
            await _commentService.UpdateCommentAsync(updateCommentDto, id , userId);
            return NoContent();
        }
        [Authorize]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteComment([FromRoute] int id)
        {
            string? userId = User.GetUserId();
            if (userId == null) return NotFound("User not found");
            await _commentService.DeleteCommentAsync(id , userId);
            return NoContent();
        }
    }
}