using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblog.Domain.JoinModels;
using Weblog.Domain.Models;

namespace Weblog.Application.Interfaces.Repositories
{
    public interface IFavoriteArticleRepository
    {
        Task<List<FavoriteArticle>> GetAllFavoriteArticlesAsync(string userId);
        Task AddArticleToFavoriteAsync(FavoriteArticle favoriteArticle);
        Task DeleteArticleFromFavoriteAsync(FavoriteArticle favoriteArticle);
        Task<bool> ArticleAddedToFavoriteAsync(FavoriteArticle favoriteArticle);
        Task<FavoriteArticle?> GetFavoriteArticleByIdAsync(int id);


    }
}