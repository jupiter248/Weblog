using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Weblog.Application.Dtos;
using Weblog.Application.Helpers;
using Weblog.Application.Interfaces.Repositories;
using Weblog.Application.Interfaces.Services;
using Weblog.Domain.Models;
using Weblog.Infrastructure.Extension;

namespace Weblog.Infrastructure.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository _articleRepo;
        private readonly IMapper _mapper;
        public ArticleService(IArticleRepository articleRepo, IMapper mapper)
        {
            _articleRepo = articleRepo;
            _mapper = mapper;
        }
        public async Task<ArticleDto> AddArticleAsync(AddArticleDto addArticleDto)
        {
            Article newArticle = _mapper.Map<Article>(addArticleDto);

            // Category checking

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

        public Task UpdateArticleAsync(UpdateArticleDto updateArticleDto)
        {
            throw new NotImplementedException();
        }

        public Task UpdateViewersAsync(int articleId)
        {
            throw new NotImplementedException();
        }
    }
}