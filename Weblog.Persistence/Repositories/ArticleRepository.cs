using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblog.Application.Helpers;
using Weblog.Application.Interfaces;
using Weblog.Application.Interfaces.IArticleRepository;
using Weblog.Domain.Models;

namespace Weblog.Persistence.Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        public Task<Article> AddArticleAsync(Article article)
        {
            throw new NotImplementedException();
        }

        public Task DeleteArticleById(Article article)
        {
            throw new NotImplementedException();
        }

        public Task<List<Article>> GetAllArticlesAsync(PaginationParams paginationParams, FilteringParams filteringParams)
        {
            throw new NotImplementedException();
        }

        public Task<Article?> GetArticleByIdAsync(int articleId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateArticleAsync(Article currentArticle, Article newArticle)
        {
            throw new NotImplementedException();
        }
    }
}