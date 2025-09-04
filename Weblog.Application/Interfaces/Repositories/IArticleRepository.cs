using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblog.Application.Queries;
using Weblog.Application.Queries.FilteringParams;
using Weblog.Domain.Models;

namespace Weblog.Application.Interfaces.Repositories
{
    public interface IArticleRepository
    {
        Task<List<Article>> GetAllArticlesAsync(PaginationParams paginationParams, ArticleFilteringParams articleFilteringParams);
        Task<Article?> GetArticleByIdAsync(int articleId);
        Task<Article> AddArticleAsync(Article article);
        Task UpdateArticleAsync(Article newArticle);
        Task DeleteArticleByIdAsync(Article article);
        Task<List<Article>> SearchByTitleAsync(string keyword);
        Task AddTagAsync(Article article, Tag tag);
        Task DeleteTagAsync(Article article, Tag tag);
        Task AddContributorAsync(Article article, Contributor contributor);
        Task DeleteContributorAsync(Article article, Contributor contributor);
        Task<bool> ArticleExistsAsync(int articleId);
        Task<List<Article>> GetSuggestionsAsync(PaginationParams paginationParams, Article article);
    }
} 