using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Weblog.Application.CustomExceptions;
using Weblog.Application.Dtos;
using Weblog.Application.Dtos.ArticleDtos;
using Weblog.Application.Interfaces.Repositories;
using Weblog.Application.Interfaces.Services;
using Weblog.Application.Queries;
using Weblog.Domain.Models;
using Weblog.Infrastructure.Extension;

namespace Weblog.Infrastructure.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository _articleRepo;
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepo;
        public ArticleService(IArticleRepository articleRepo, IMapper mapper, ICategoryRepository categoryRepo)
        {
            _articleRepo = articleRepo;
            _mapper = mapper;
            _categoryRepo = categoryRepo;
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

        public Task DeleteArticleAsync(int articleId)
        {
            throw new NotImplementedException();
        }

        public Task<List<ArticleDto>> GetAllArticlesAsync(PaginationParams paginationParams, FilteringParams filteringParams)
        {
            throw new NotImplementedException();
        }

        public Task<ArticleDto> GetArticleByIdAsync(int articleId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateArticleAsync(UpdateArticleDto updateArticleDto , int categoryId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateViewersAsync(int articleId)
        {
            throw new NotImplementedException();
        }
    }
}