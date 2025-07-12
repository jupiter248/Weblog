using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Weblog.Application.CustomExceptions;
using Weblog.Application.Dtos.ArticleDtos;
using Weblog.Application.Interfaces.Repositories;
using Weblog.Application.Interfaces.Services;
using Weblog.Domain.Enums;
using Weblog.Domain.JoinModels;
using Weblog.Domain.JoinModels.Favorites;
using Weblog.Domain.Models;

namespace Weblog.Infrastructure.Services
{
    public class FavoriteArticleService : IFavoriteArticleService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IFavoriteArticleRepository _favoriteArticleRepo;
        private readonly IArticleRepository _articleRepo;
        private readonly IMapper _mapper;

        public FavoriteArticleService(IArticleRepository articleRepository, IMapper mapper, UserManager<AppUser> userManager, IFavoriteArticleRepository favoriteArticleRepo)
        {
            _userManager = userManager;
            _favoriteArticleRepo = favoriteArticleRepo;
            _mapper = mapper;
            _articleRepo = articleRepository;
        }

        public async Task AddArticleToFavoriteAsync(int articleId, string userId , int favoriteListId)
        {
            AppUser appUser = await _userManager.FindByIdAsync(userId) ?? throw new NotFoundException("User not found");
            Article article = await _articleRepo.GetArticleByIdAsync(articleId) ?? throw new NotFoundException("Article not found");
            bool articleAdded = await _favoriteArticleRepo.ArticleAddedToFavoriteAsync(new FavoriteArticle { ArticleId = articleId, UserId = userId });
            if(articleAdded == true)
            {
                throw new ValidationException("The article already added into favorites");
            }
            FavoriteArticle favoriteArticle = new FavoriteArticle
            {
                UserId = appUser.Id,
                AppUser = appUser,
                ArticleId = articleId,
                Article = article
            };
            await _favoriteArticleRepo.AddArticleToFavoriteAsync(favoriteArticle);
        }

        public async Task DeleteArticleFromFavoriteAsync(int favoriteArticleId, string userId)
        {
            AppUser appUser = await _userManager.FindByIdAsync(userId) ?? throw new NotFoundException("User not found");
            FavoriteArticle favoriteArticle = await _favoriteArticleRepo.GetFavoriteArticleByIdAsync(favoriteArticleId) ?? throw new NotFoundException("Favorite article not found");
            if (appUser.Id != favoriteArticle.UserId)
            {
                throw new ValidationException("Favorite article not found");
            }

            await _favoriteArticleRepo.DeleteArticleFromFavoriteAsync(favoriteArticle);
        }

        public async Task<List<ArticleDto>> GetAllFavoriteArticlesAsync(string userId)
        {
            AppUser appUser = await _userManager.FindByIdAsync(userId) ?? throw new NotFoundException("User not found");
            List<FavoriteArticle> favoriteArticles = await _favoriteArticleRepo.GetAllFavoriteArticlesAsync(userId);
            List<ArticleDto> articleDtos = _mapper.Map<List<ArticleDto>>(favoriteArticles.Select(f => f.Article));
            return articleDtos;
        }
    }
}