using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Weblog.Application.CustomExceptions;
using Weblog.Application.Dtos.ArticleDtos;
using Weblog.Application.Dtos.FavoritesDtos.ArticleFavoriteDto;
using Weblog.Application.Interfaces.Repositories;
using Weblog.Application.Interfaces.Services;
using Weblog.Application.Queries;
using Weblog.Application.Queries.FilteringParams;
using Weblog.Domain.Enums;
using Weblog.Domain.Errors;
using Weblog.Domain.Errors.Favorite;
using Weblog.Domain.Errors.User;
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
            AppUser appUser = await _userManager.FindByIdAsync(userId) ?? throw new NotFoundException(UserErrorCodes.UserNotFound);
            Article article = await _articleRepo.GetArticleByIdAsync(addFavoriteArticleDto.ArticleId) ?? throw new NotFoundException(ArticleErrorCodes.ArticleNotFound);
            if (addFavoriteArticleDto.favoriteListId.HasValue)
            {
                FavoriteList favoriteList = await _favoriteListRepo.GetFavoriteListByIdAsync(addFavoriteArticleDto.ArticleId) ?? throw new NotFoundException(FavoriteErrorCodes.FavoriteListNotFound); 
            }
           
            bool articleAdded = await _favoriteArticleRepo.ArticleAddedToFavoriteAsync(new FavoriteArticle { ArticleId = addFavoriteArticleDto.ArticleId, UserId = userId });
            if(articleAdded == true)
            {
                throw new ConflictException(FavoriteErrorCodes.FavoriteItemAlreadyExists);
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
            AppUser appUser = await _userManager.FindByIdAsync(userId) ?? throw new NotFoundException(UserErrorCodes.UserNotFound);
            FavoriteArticle favoriteArticle = await _favoriteArticleRepo.GetFavoriteArticleByIdAsync(favoriteArticleId) ?? throw new NotFoundException("Favorite article not found");
            if (appUser.Id != favoriteArticle.UserId)
            {
                throw new NotFoundException(FavoriteErrorCodes.FavoriteItemNotFound);
            }

            await _favoriteArticleRepo.DeleteArticleFromFavoriteAsync(favoriteArticle);
        }

        public async Task<List<ArticleSummaryDto>> GetAllFavoriteArticlesAsync(string userId , FavoriteFilteringParams favoriteFilteringParams , PaginationParams paginationParams)
        {
            AppUser appUser = await _userManager.FindByIdAsync(userId) ?? throw new NotFoundException(UserErrorCodes.UserNotFound);
            List<FavoriteArticle> favoriteArticles = await _favoriteArticleRepo.GetAllFavoriteArticlesAsync(userId ,favoriteFilteringParams ,paginationParams);
            List<ArticleSummaryDto> articleDtos = _mapper.Map<List<ArticleSummaryDto>>(favoriteArticles.Select(f => f.Article));
            return articleDtos;
        }
    }
}