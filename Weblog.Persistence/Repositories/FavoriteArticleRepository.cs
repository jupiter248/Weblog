using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Weblog.Application.Interfaces.Repositories;
using Weblog.Application.Queries;
using Weblog.Application.Queries.FilteringParams;
using Weblog.Domain.JoinModels;
using Weblog.Domain.Models;
using Weblog.Persistence.Data;

namespace Weblog.Persistence.Repositories
{
    public class FavoriteArticleRepository : IFavoriteArticleRepository
    {
        private readonly ApplicationDbContext _context;
        public FavoriteArticleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddArticleToFavoriteAsync(FavoriteArticle favoriteArticle)
        {
            await _context.FavoriteArticles.AddAsync(favoriteArticle);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsArticleFavoriteAsync(FavoriteArticle favoriteArticle)
        {
            FavoriteArticle? favoriteArticleExists = await _context.FavoriteArticles.FirstOrDefaultAsync(f => f.UserId == favoriteArticle.UserId && f.ArticleId == favoriteArticle.ArticleId);
            if (favoriteArticleExists == null)
            {
                return false;
            }
            return true;
        }

        public async Task DeleteArticleFromFavoriteAsync(FavoriteArticle favoriteArticle)
        {
            _context.FavoriteArticles.Remove(favoriteArticle);
            await _context.SaveChangesAsync();
        }
        public async Task<List<FavoriteArticle>> GetAllFavoriteArticlesAsync(string userId, FavoriteFilteringParams favoriteFilteringParams, PaginationParams paginationParams)
        {
            var favoriteArticleQuery = _context.FavoriteArticles.Include(a => a.Article).ThenInclude(m => m.Media).Where(a => a.UserId == userId).AsQueryable();
            if (favoriteFilteringParams.FavoriteListId.HasValue)
            {
                favoriteArticleQuery = favoriteArticleQuery.Where(f => f.FavoriteListId == favoriteFilteringParams.FavoriteListId);
            }

            if (favoriteFilteringParams.NewestArrivals == true)
            {
                favoriteArticleQuery = favoriteArticleQuery.OrderByDescending(a => a.AddedAt);
            }
            else
            {
                favoriteArticleQuery = favoriteArticleQuery.OrderBy(a => a.AddedAt);
            }

            List<FavoriteArticle> favoriteArticles = await favoriteArticleQuery.ToListAsync();
            var skipNumber = (paginationParams.PageNumber - 1) * paginationParams.PageSize;

            return favoriteArticles.Skip(skipNumber).Take(paginationParams.PageSize).ToList();
        }

        public async Task<FavoriteArticle?> GetFavoriteArticleByIdAsync(int id)
        {
            FavoriteArticle? favoriteArticle = await _context.FavoriteArticles.FirstOrDefaultAsync(f => f.Id == id);
            if (favoriteArticle == null)
            {
                return null;
            }
            return favoriteArticle;
        }
    }
}