using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Weblog.Application.CustomExceptions;
using Weblog.Application.Dtos.ArticleDtos;
using Weblog.Application.Dtos.FavoritesDtos.ArticleFavoriteDto;
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
        private readonly IFavoriteListRepository _favoriteListRepo;

        private readonly IMapper _mapper;

        public FavoriteArticleService(IFavoriteListRepository favoriteListRepo, IArticleRepository articleRepository, IMapper mapper, UserManager<AppUser> userManager, IFavoriteArticleRepository favoriteArticleRepo)
        {
            _userManager = userManager;
            _favoriteArticleRepo = favoriteArticleRepo;
            _mapper = mapper;
            _articleRepo = articleRepository;
            _favoriteListRepo = favoriteListRepo;
        }

        public async Task AddArticleToFavoriteAsync(string userId,AddFavoriteArticleDto addFavoriteArticleDto)
        {
            AppUser appUser = await _userManager.FindByIdAsync(userId) ?? throw new NotFoundException("User not found");
            Article article = await _articleRepo.GetArticleByIdAsync(addFavoriteArticleDto.ArticleId) ?? throw new NotFoundException("Article not found");
            if (addFavoriteArticleDto.favoriteListId.HasValue)
            {
                FavoriteList favoriteList = await _favoriteListRepo.GetFavoriteListByIdAsync(addFavoriteArticleDto.ArticleId) ?? throw new NotFoundException("Favorite list not found"); 
            }
           
            bool articleAdded = await _favoriteArticleRepo.ArticleAddedToFavoriteAsync(new FavoriteArticle { ArticleId = addFavoriteArticleDto.ArticleId, UserId = userId });
            if(articleAdded == true)
            {
                throw new ValidationException("The article already added into favorites");
            }
            FavoriteArticle favoriteArticle = new FavoriteArticle
            {
                UserId = appUser.Id,
                AppUser = appUser,
                ArticleId = article.Id,
                Article = article,
                FavoriteListId = addFavoriteArticleDto.favoriteListId
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

        public async Task<List<ArticleDto>> GetAllFavoriteArticlesAsync(string userId , int? favoriteListId)
        {
            AppUser appUser = await _userManager.FindByIdAsync(userId) ?? throw new NotFoundException("User not found");
            List<FavoriteArticle> favoriteArticles = await _favoriteArticleRepo.GetAllFavoriteArticlesAsync(userId);
            if (favoriteListId.HasValue)
            {
                favoriteArticles = favoriteArticles = favoriteArticles.Where(f => f.FavoriteListId == favoriteListId).ToList();
            }
            List<ArticleDto> articleDtos = _mapper.Map<List<ArticleDto>>(favoriteArticles.Select(f => f.Article));
            return articleDtos;
        }
    }
}