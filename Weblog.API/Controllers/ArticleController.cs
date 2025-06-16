using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Weblog.Application.Dtos;
using Weblog.Application.Dtos.ArticleDtos;
using Weblog.Application.Interfaces.Services;

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
        [HttpPost]
        public async Task<IActionResult> AddArticle([FromBody] AddArticleDto addArticleDto)
        {
            ArticleDto articleDto = await _articleService.AddArticleAsync(addArticleDto);
            return Ok(articleDto);
        }
    }
}