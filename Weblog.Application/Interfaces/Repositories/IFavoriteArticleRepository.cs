using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblog.Application.Queries;
using Weblog.Application.Queries.FilteringParams;
using Weblog.Domain.JoinModels;
using Weblog.Domain.Models;

namespace Weblog.Application.Interfaces.Repositories
{
    public interface IFavoriteArticleRepository
    {
        Task<List<FavoriteArticle>> GetAllFavoriteArticlesAsync(string userId , FavoriteFilteringParams favoriteFilteringParams , PaginationParams paginationParams);
        Task AddArticleToFavoriteAsync(FavoriteArticle favoriteArticle);
        Task DeleteArticleFromFavoriteAsync(FavoriteArticle favoriteArticle);
        Task<bool> IsArticleFavoriteAsync(FavoriteArticle favoriteArticle);
        Task<FavoriteArticle?> GetFavoriteArticleByIdAsync(int id);


    }
}