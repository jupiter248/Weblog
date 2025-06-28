using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblog.Application.Dtos;
using Weblog.Application.Dtos.ArticleDtos;
using Weblog.Application.Queries;
using Weblog.Domain.Models;

namespace Weblog.Application.Interfaces.Services
{
    public interface IArticleService
    {
        Task<List<ArticleDto>> GetAllArticlesAsync(PaginationParams paginationParams, FilteringParams filteringParams);
        Task<ArticleDto> GetArticleByIdAsync(int articleId);
        Task<ArticleDto> AddArticleAsync(AddArticleDto addArticleDto);
        Task UpdateViewersAsync(int articleId);
        Task UpdateLikesAsync(int articleId);
        Task UpdateArticleAsync(UpdateArticleDto updateArticleDto, int articleId);
        Task DeleteArticleAsync(int articleId);
        Task AddTagAsync(int articleId , int tagId);
        Task DeleteTagAsync(int articleId , int tagId);
        Task AddContributorAsync(int articleId , int contributorId);
        Task DeleteContributorAsync(int articleId , int contributorId);
    }
}