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
using Weblog.Domain.Models;
using Weblog.Infrastructure.Extension;

namespace Weblog.Infrastructure.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository _articleRepo;
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepo;
        private readonly ITagRepository _tagRepo;
        private readonly IContributorRepository _contributorRepo;

        public ArticleService(IArticleRepository articleRepo, IMapper mapper, IContributorRepository contributorRepo, ICategoryRepository categoryRepo, ITagRepository tagRepo)
        {
            _articleRepo = articleRepo;
            _mapper = mapper;
            _categoryRepo = categoryRepo;
            _tagRepo = tagRepo;
            _contributorRepo = contributorRepo;
        }
        public async Task<ArticleDto> AddArticleAsync(AddArticleDto addArticleDto)
        {
            Article newArticle = _mapper.Map<Article>(addArticleDto);

            Category? category = await _categoryRepo.GetCategoryByIdAsync(addArticleDto.CategoryId) ?? throw new NotFoundException("Category not found");
            newArticle.Category = category;
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
            Article article = await _articleRepo.GetArticleByIdAsync(articleId) ?? throw new NotFoundException("Article not found");
            await _articleRepo.DeleteArticleByIdAsync(article);
        }

        public async Task<List<ArticleSummaryDto>> GetAllArticlesAsync(PaginationParams paginationParams, FilteringParams filteringParams)
        {
            List<Article> articles = await _articleRepo.GetAllArticlesAsync(paginationParams, filteringParams);
            List<ArticleSummaryDto> articleDtos = _mapper.Map<List<ArticleSummaryDto>>(articles);
            return articleDtos;
        }

        public async Task<ArticleDto> GetArticleByIdAsync(int articleId)
        {
            Article article = await _articleRepo.GetArticleByIdAsync(articleId) ?? throw new NotFoundException("Article not found");
            return _mapper.Map<ArticleDto>(article);
        }

        public async Task UpdateArticleAsync(UpdateArticleDto updateArticleDto, int articleId)
        {
            Article currentArticle = await _articleRepo.GetArticleByIdAsync(articleId) ?? throw new NotFoundException("Article not found");
            Category category = await _categoryRepo.GetCategoryByIdAsync(updateArticleDto.CategoryId) ?? throw new NotFoundException("Category not found");
            Article newArticle = _mapper.Map<Article>(updateArticleDto);
            newArticle.Category = category;
            newArticle.CategoryId = category.Id;
            await _articleRepo.UpdateArticleAsync(currentArticle, newArticle);
        }

        public async Task UpdateViewersAsync(int articleId)
        {
            Article article = await _articleRepo.GetArticleByIdAsync(articleId) ?? throw new NotFoundException("Article not found");
            await _articleRepo.UpdateViewersAsync(article);
        }
        public async Task UpdateLikesAsync(int articleId)
        {
            Article article = await _articleRepo.GetArticleByIdAsync(articleId) ?? throw new NotFoundException("Article not found");
            await _articleRepo.UpdateLikesAsync(article);
        }

        public async Task AddTagAsync(int articleId, int tagId)
        {
            Article article = await _articleRepo.GetArticleByIdAsync(articleId) ?? throw new NotFoundException("Article not found");
            Tag tag = await _tagRepo.GetTagByIdAsync(tagId) ?? throw new NotFoundException("Tag not found");
            await _articleRepo.AddTagAsync(article , tag);
        }

        public async Task DeleteTagAsync(int articleId, int tagId)
        {
            Article article = await _articleRepo.GetArticleByIdAsync(articleId) ?? throw new NotFoundException("Article not found");
            Tag tag = await _tagRepo.GetTagByIdAsync(tagId) ?? throw new NotFoundException("Tag not found");
            await _articleRepo.DeleteTagAsync(article , tag);
        }

        public async Task AddContributorAsync(int articleId, int contributorId)
        {
            Article article = await _articleRepo.GetArticleByIdAsync(articleId) ?? throw new NotFoundException("Article not found");
            Contributor contributor = await _contributorRepo.GetContributorByIdAsync(contributorId) ?? throw new NotFoundException("Contributor not found");
            await _articleRepo.AddContributorAsync(article , contributor);
        }

        public async Task DeleteContributorAsync(int articleId, int contributorId)
        {
            Article article = await _articleRepo.GetArticleByIdAsync(articleId) ?? throw new NotFoundException("Article not found");
            Contributor contributor = await _contributorRepo.GetContributorByIdAsync(contributorId) ?? throw new NotFoundException("Contributor not found");
            await _articleRepo.DeleteContributorAsync(article , contributor);
        }
    }
}