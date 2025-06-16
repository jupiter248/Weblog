using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblog.Application.Dtos;
using Weblog.Application.Dtos.ArticleDtos;
using Weblog.Application.Queries;

namespace Weblog.Application.Interfaces.Services
{
    public interface IArticleService
    {
        Task<List<ArticleDto>> GetAllArticlesAsync(PaginationParams paginationParams, FilteringParams filteringParams);
        Task<ArticleDto> GetArticleByIdAsync(int articleId);
        Task<ArticleDto> AddArticleAsync(AddArticleDto addArticleDto);
        Task UpdateViewersAsync(int articleId);
        Task UpdateArticleAsync(UpdateArticleDto updateArticleDto);
        Task DeleteArticleAsync(int articleId);
        // Add a tag
        // Delete a tag 
        // Add a contributor
        // Delete a contributor
    }
}