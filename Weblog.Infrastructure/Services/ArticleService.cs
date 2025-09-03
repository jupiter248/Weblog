using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Weblog.Application.CustomExceptions;
using Weblog.Application.Dtos;
using Weblog.Application.Dtos.ArticleDtos;
using Weblog.Application.Dtos.TagDtos;
using Weblog.Application.Interfaces.Repositories;
using Weblog.Application.Interfaces.Services;
using Weblog.Application.Queries;
using Weblog.Application.Queries.FilteringParams;
using Weblog.Domain.Enums;
using Weblog.Domain.Models;
using Weblog.Domain.Errors;
using Weblog.Infrastructure.Extension;
using Weblog.Domain.Errors.Category;
using Weblog.Domain.Errors.Contributor;
using Weblog.Domain.Errors.Common;

namespace Weblog.Infrastructure.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository _articleRepo;
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepo;
        private readonly ITagRepository _tagRepo;
        private readonly IContributorRepository _contributorRepo;
        private readonly ILikeContentRepository _likeContentRepo;
        private readonly IViewContentRepository _viewContentRepo;




        public ArticleService(IViewContentRepository viewContentRepo, ILikeContentRepository likeContentRepo, IArticleRepository articleRepo, IMapper mapper, IContributorRepository contributorRepo, ICategoryRepository categoryRepo, ITagRepository tagRepo)
        {
            _articleRepo = articleRepo;
            _mapper = mapper;
            _categoryRepo = categoryRepo;
            _tagRepo = tagRepo;
            _contributorRepo = contributorRepo;
            _likeContentRepo = likeContentRepo;
            _viewContentRepo = viewContentRepo;
        }
        public async Task<ArticleDto> AddArticleAsync(AddArticleDto addArticleDto)
        {
            Article newArticle = _mapper.Map<Article>(addArticleDto);
            Category? category = await _categoryRepo.GetCategoryByIdAsync(addArticleDto.CategoryId) ?? throw new NotFoundException(ArticleErrorCodes.ArticleNotFound);
            if (category.EntityType == CategoryType.Article)
            {
                newArticle.Category = category;
            }
            else
            {
                throw new ConflictException(CategoryErrorCodes.CategoryEntityTypeMatchFailed);
            }
            newArticle.Slug = newArticle.Title.Slugify();

            if (newArticle.IsPublished)
            {
                newArticle.PublishedAt = DateTimeOffset.Now;
            }

            Article addedArticle = await _articleRepo.AddArticleAsync(newArticle);
            return _mapper.Map<ArticleDto>(addedArticle);
        }

        public async Task DeleteArticleAsync(int articleId)
        {
            Article article = await _articleRepo.GetArticleByIdAsync(articleId) ?? throw new NotFoundException(ArticleErrorCodes.ArticleNotFound);
            await _articleRepo.DeleteArticleByIdAsync(article);
        }
        public async Task<List<ArticleSummaryDto>> GetAllArticlesAsync(PaginationParams paginationParams, ArticleFilteringParams articleFilteringParams)
        {
            List<Article> articles = await _articleRepo.GetAllArticlesAsync(paginationParams, articleFilteringParams);
            List<ArticleSummaryDto> articleSummaryDtos = _mapper.Map<List<ArticleSummaryDto>>(articles);

            foreach (var item in articleSummaryDtos)
            {
                item.LikeCount = await _likeContentRepo.GetLikeCountAsync(item.Id, LikeAndViewType.Article);
                item.ViewCount = await _viewContentRepo.GetViewCountAsync(item.Id, LikeAndViewType.Article);
            }

            if (articleFilteringParams.MostLikes == true)
            {
                articleSummaryDtos = articleSummaryDtos.OrderByDescending(l => l.LikeCount).ToList();
            }
            else if (articleFilteringParams.MostLikes == false)
            {
                articleSummaryDtos = articleSummaryDtos.OrderBy(l => l.LikeCount).ToList();
            }

            if (articleFilteringParams.MostViews == true)
            {
                articleSummaryDtos = articleSummaryDtos.OrderByDescending(l => l.ViewCount).ToList();
            }
            else if (articleFilteringParams.MostViews == false)
            {
                articleSummaryDtos = articleSummaryDtos.OrderBy(l => l.ViewCount).ToList();
            }
            return articleSummaryDtos;
        }

        public async Task<ArticleDto> GetArticleByIdAsync(int articleId)
        {
            Article article = await _articleRepo.GetArticleByIdAsync(articleId) ?? throw new NotFoundException(ArticleErrorCodes.ArticleNotFound);
            ArticleDto articleDto = _mapper.Map<ArticleDto>(article);
            articleDto.LikeCount = await _likeContentRepo.GetLikeCountAsync(articleDto.Id, LikeAndViewType.Article);
            articleDto.ViewCount = await _viewContentRepo.GetViewCountAsync(articleDto.Id, LikeAndViewType.Article);
            return articleDto;
        }

        public async Task UpdateArticleAsync(UpdateArticleDto updateArticleDto, int articleId)
        {
            Article currentArticle = await _articleRepo.GetArticleByIdAsync(articleId) ?? throw new NotFoundException(ArticleErrorCodes.ArticleNotFound);
            Category category = await _categoryRepo.GetCategoryByIdAsync(updateArticleDto.CategoryId) ?? throw new NotFoundException(CategoryErrorCodes.CategoryNotFound);
            Article newArticle = _mapper.Map<Article>(updateArticleDto);
            newArticle.Category = category;
            newArticle.CategoryId = category.Id;
            newArticle.Slug = updateArticleDto.Title.Slugify();
            await _articleRepo.UpdateArticleAsync(currentArticle, newArticle);
        }
        public async Task AddTagAsync(int articleId, int tagId)
        {
            Article article = await _articleRepo.GetArticleByIdAsync(articleId) ?? throw new NotFoundException(ArticleErrorCodes.ArticleNotFound);
            Tag tag = await _tagRepo.GetTagByIdAsync(tagId) ?? throw new NotFoundException("Tag not found");
            await _articleRepo.AddTagAsync(article , tag);
        }

        public async Task DeleteTagAsync(int articleId, int tagId)
        {
            Article article = await _articleRepo.GetArticleByIdAsync(articleId) ?? throw new NotFoundException(ArticleErrorCodes.ArticleNotFound);
            Tag tag = await _tagRepo.GetTagByIdAsync(tagId) ?? throw new NotFoundException("Tag not found");
            await _articleRepo.DeleteTagAsync(article , tag);
        }

        public async Task AddContributorAsync(int articleId, int contributorId)
        {
            Article article = await _articleRepo.GetArticleByIdAsync(articleId) ?? throw new NotFoundException(ArticleErrorCodes.ArticleNotFound);
            Contributor contributor = await _contributorRepo.GetContributorByIdAsync(contributorId) ?? throw new NotFoundException(ContributorErrorCodes.ContributorNotFound);
            await _articleRepo.AddContributorAsync(article , contributor);
        }

        public async Task DeleteContributorAsync(int articleId, int contributorId)
        {
            Article article = await _articleRepo.GetArticleByIdAsync(articleId) ?? throw new NotFoundException(ArticleErrorCodes.ArticleNotFound);
            Contributor contributor = await _contributorRepo.GetContributorByIdAsync(contributorId) ?? throw new NotFoundException(ContributorErrorCodes.ContributorNotFound);
            await _articleRepo.DeleteContributorAsync(article , contributor);
        }

        public async Task<List<ArticleSummaryDto>> GetSuggestionsAsync(PaginationParams paginationParams, int articleId)
        {
            Article article = await _articleRepo.GetArticleByIdAsync(articleId) ?? throw new NotFoundException(ArticleErrorCodes.ArticleNotFound);
            List<Article> articles = await _articleRepo.GetSuggestionsAsync(paginationParams, article);
            List<ArticleSummaryDto> articleSummaryDto = _mapper.Map<List<ArticleSummaryDto>>(articles);
            foreach (var item in articleSummaryDto)
            {
                item.LikeCount = await _likeContentRepo.GetLikeCountAsync(item.Id, LikeAndViewType.Article);
                item.ViewCount = await _viewContentRepo.GetViewCountAsync(item.Id, LikeAndViewType.Article);
            }
            return articleSummaryDto;
        }
    }
}