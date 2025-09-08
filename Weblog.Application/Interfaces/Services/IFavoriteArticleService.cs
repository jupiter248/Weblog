using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblog.Application.Dtos.ArticleDtos;
using Weblog.Application.Dtos.FavoritesDtos.ArticleFavoriteDto;
using Weblog.Application.Queries;
using Weblog.Application.Queries.FilteringParams;
using Weblog.Domain.Errors;

namespace Weblog.Application.Interfaces.Services
{
    public interface IFavoriteArticleService
    {
        Task<List<ArticleSummaryDto>> GetAllFavoriteArticlesAsync(string userId, FavoriteFilteringParams favoriteFilteringParams, PaginationParams paginationParams);
        Task AddArticleToFavoriteAsync(string userId, AddFavoriteArticleDto addFavoriteArticleDto);
        Task DeleteArticleFromFavoriteAsync(int articleId, string userId);
        Task<bool> IsArticleFavoriteAsync(string userId , int articleId);
    }
}