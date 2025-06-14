using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblog.Application.Dtos;
using Weblog.Application.Helpers;
using Weblog.Application.Interfaces.Services;

namespace Weblog.Infrastructure.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleService _articleService;
        public ArticleService(IArticleService articleService)
        {
            _articleService = articleService;
        }
        public Task<ArticleDto> AddArticleAsync(AddArticleDto addArticleDto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteArticleAsync(int articleId)
        {
            throw new NotImplementedException();
        }

        public Task<List<ArticleDto>> GetAllArticlesAsync(PaginationParams paginationParams, FilteringParams filteringParams)
        {
            throw new NotImplementedException();
        }

        public Task<ArticleDto> GetArticleByIdAsync(int articleId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateArticleAsync(UpdateArticleDto updateArticleDto)
        {
            throw new NotImplementedException();
        }

        public Task UpdateViewersAsync(int articleId)
        {
            throw new NotImplementedException();
        }
    }
}