using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblog.Application.Dtos.ArticleDtos;

namespace Weblog.Application.Interfaces.Services
{
    public interface IFavoriteArticleService
    {
        Task<List<ArticleDto>> GetAllFavoriteArticlesAsync(string userId);
        Task AddArticleToFavoriteAsync(int articleId , string userId);
        Task DeleteArticleFromFavoriteAsync(int articleId , string userId);
    }
}