using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblog.Application.Queries;
using Weblog.Domain.Models;

namespace Weblog.Application.Interfaces.Repositories
{
    public interface IArticleRepository
    {
        Task<List<Article>> GetAllArticlesAsync(PaginationParams paginationParams, FilteringParams filteringParams);
        Task<Article?> GetArticleByIdAsync(int articleId);
        Task<Article> AddArticleAsync(Article article);
        Task UpdateViewersAsync(Article article);
        Task UpdateLikesAsync(Article article);
        Task UpdateArticleAsync(Article currentArticle, Article newArticle);
        Task DeleteArticleByIdAsync(Article article);
        Task AddTagAsync(Article article, Tag tag);
        Task DeleteTagAsync(Article article, Tag tag);
        Task AddContributorAsync(Article article, Contributor contributor);
        Task DeleteContributorAsync(Article article, Contributor contributor);

    }
} 