using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Weblog.Application.Interfaces;
using Weblog.Application.Interfaces.Repositories;
using Weblog.Application.Queries;
using Weblog.Domain.Models;
using Weblog.Persistence.Data;

namespace Weblog.Persistence.Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly ApplicationDbContext _context;
        public ArticleRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Article> AddArticleAsync(Article article)
        {
            await _context.Articles.AddAsync(article);
            await _context.SaveChangesAsync();
            return article;
        }

        public async Task DeleteArticleByIdAsync(Article article)
        {
            _context.Articles.Remove(article);
            await _context.SaveChangesAsync();
        }

        public List<Article> GetAllArticlesAsync(PaginationParams paginationParams, FilteringParams filteringParams)
        {
            var articles = _context.Articles.Include(m => m.Media).Include(t => t.Tags).Include(c => c.Contributors).AsQueryable();

            if (!string.IsNullOrWhiteSpace(filteringParams.Title))
            {
                articles = articles.Where(p => p.Title.ToLower().Contains(filteringParams.Title.ToLower().Replace(" ", "")));
            }
            if (filteringParams.CategoryId.HasValue)
            {
                articles = articles.Where(a => a.CategoryId == filteringParams.CategoryId);
            }

            var skipNumber = (paginationParams.PageNumber - 1) * paginationParams.PageSize;

            return articles.Skip(skipNumber).Take(paginationParams.PageSize).ToList();

        }

        public async Task<Article?> GetArticleByIdAsync(int articleId)
        {
            Article? article = await _context.Articles.FirstOrDefaultAsync(a => a.Id == articleId);
            if (article == null)
            {
                return null;
            }
            return article;
        }

        public async Task UpdateArticleAsync(Article currentArticle, Article newArticle)
        {
            currentArticle.Title = newArticle.Title;
            currentArticle.Slug = newArticle.Slug;
            currentArticle.Context = newArticle.Context;
            currentArticle.UpdatedAt = DateTime.Now;
            currentArticle.IsPublished = newArticle.IsPublished;
            currentArticle.CategoryId = newArticle.CategoryId;
            currentArticle.Category = newArticle.Category;
            await _context.SaveChangesAsync();
        }

        public async Task UpdateViewersAsync(Article article)
        {
            article.Viewers++;
            await _context.SaveChangesAsync();
        }
    }
}