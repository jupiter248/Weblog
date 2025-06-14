using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblog.Application.Helpers;
using Weblog.Domain.Models;

namespace Weblog.Application.Interfaces.IArticleRepository
{
    public interface IArticleRepository
    {
        List<Article> GetAllArticlesAsync(PaginationParams paginationParams, FilteringParams filteringParams);
        Task<Article?> GetArticleByIdAsync(int articleId);
        Task<Article> AddArticleAsync(Article article);
        Task UpdateViewersAsync(Article article);
        Task UpdateArticleAsync(Article currentArticle, Article newArticle);
        Task DeleteArticleByIdAsync(Article article);
            // Add a tag
            // Delete a tag 
            // Add a contributor
            // Delete a contributor

    }
} 