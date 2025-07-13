using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblog.Application.Dtos.ArticleDtos;
using Weblog.Application.Dtos.FavoritesDtos.ArticleFavoriteDto;

namespace Weblog.Application.Interfaces.Services
{
    public interface IFavoriteArticleService
    {
        Task<List<ArticleDto>> GetAllFavoriteArticlesAsync(string userId , int? favoriteListId);
        Task AddArticleToFavoriteAsync(string userId ,AddFavoriteArticleDto addFavoriteArticleDto);
        Task DeleteArticleFromFavoriteAsync(int articleId , string userId);
    }
}