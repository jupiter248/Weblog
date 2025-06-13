using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblog.Application.Dtos;
using Weblog.Application.Helpers;

namespace Weblog.Application.Interfaces.Services
{
    public interface IArticleService
    {
        Task<List<ArticleDto>> GetAllArticlesAsync(PaginationParams paginationParams, FilteringParams filteringParams);
        Task<ArticleDto> GetArticleByIdAsync(int articleId);
        Task<ArticleDto> AddArticleAsync(AddArticleDto addArticleDto);
        // Add viewers method
        Task UpdateArticleAsync(UpdateArticleDto updateArticleDto);
        Task DeleteArticleAsync(int articleId);
    }
}