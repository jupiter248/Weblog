using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblog.Application.Dtos;
using Weblog.Application.Dtos.ArticleDtos;
using Weblog.Application.Queries;
using Weblog.Application.Queries.FilteringParams;
using Weblog.Domain.Models;

namespace Weblog.Application.Interfaces.Services
{
    public interface IArticleService
    {
        Task<List<ArticleSummaryDto>> GetAllArticlesAsync(PaginationParams paginationParams, ArticleFilteringParams articleFilteringParams);
        Task<ArticleDto> GetArticleByIdAsync(int articleId);
        Task<ArticleDto> AddArticleAsync(AddArticleDto addArticleDto);
        Task<ArticleDto> UpdateArticleAsync(UpdateArticleDto updateArticleDto, int articleId);
        Task<int> IncrementArticleViewAsync(int articleId);
        Task DeleteArticleAsync(int articleId);
        Task AddTagAsync(int articleId, int tagId);
        Task DeleteTagAsync(int articleId, int tagId);
        Task AddContributorAsync(int articleId, int contributorId);
        Task DeleteContributorAsync(int articleId, int contributorId);
        Task<List<ArticleSummaryDto>> GetSuggestionsAsync(PaginationParams paginationParams, int articleId);

    }
}