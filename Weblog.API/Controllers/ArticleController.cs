using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Weblog.Application.Dtos;
using Weblog.Application.Dtos.ArticleDtos;
using Weblog.Application.Interfaces.Services;
using Weblog.Application.Queries;
using Weblog.Application.Queries.FilteringParams;

namespace Weblog.API.Controllers
{
    [ApiController]
    [Route("api/article")]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleService _articleService;
        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllArticles([FromQuery] FilteringParams filteringParams, [FromQuery] PaginationParams paginationParams)
        {
            List<ArticleDto> articleDtos = await _articleService.GetAllArticlesAsync(paginationParams, filteringParams);
            return Ok(articleDtos);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetArticleById(int id)
        {
            ArticleDto articleDto = await _articleService.GetArticleByIdAsync(id);
            return Ok(articleDto);
        }
        [HttpPost]
        public async Task<IActionResult> AddArticle([FromBody] AddArticleDto addArticleDto)
        {
            ArticleDto articleDto = await _articleService.AddArticleAsync(addArticleDto);
            return CreatedAtAction(nameof(GetArticleById), new { id = articleDto.Id }, articleDto);
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateArticle(int id, UpdateArticleDto updateArticleDto)
        {
            await _articleService.UpdateArticleAsync(updateArticleDto, id);
            return NoContent();
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteArticle(int id)
        {
            await _articleService.DeleteArticleAsync(id);
            return NoContent();
        }
        [HttpPut("{id:int}/viewer")]
        public async Task<IActionResult> UpdateViewers(int id)
        {
            await _articleService.UpdateViewersAsync(id);
            return NoContent(); 
        }
        [HttpPut("{id:int}/like")]
        public async Task<IActionResult> UpdateLikes(int id)
        {
            await _articleService.UpdateLikesAsync(id);
            return NoContent();
        }
        [HttpPost("{id:int}/tag")]
        public async Task<IActionResult> AddTagToArticle(int id, int tagId)
        {
            await _articleService.AddTagAsync(id, tagId);
            return NoContent();
        }
        [HttpDelete("{id:int}/tag")]
        public async Task<IActionResult> DeleteTagOfArticle(int id, int tagId)
        {
            await _articleService.DeleteTagAsync(id, tagId);
            return NoContent();
        }
        [HttpPost("{id:int}/contributor")]
        public async Task<IActionResult> AddContributorToArticle(int id, int contributorId)
        {
            await _articleService.AddContributorAsync(id, contributorId);
            return NoContent();
        }
        [HttpDelete("{id:int}/contributor")]
        public async Task<IActionResult> DeleteContributorOfArticle(int id, int contributorId)
        {
            await _articleService.DeleteContributorAsync(id , contributorId);
            return NoContent();
        }
    }
}