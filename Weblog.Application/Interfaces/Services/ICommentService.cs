using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblog.Application.Dtos.CommentDtos;
using Weblog.Application.Queries;
using Weblog.Application.Queries.FilteringParams;

namespace Weblog.Application.Interfaces.Services
{
    public interface ICommentService
    {
        Task<List<CommentDto>> GetAllCommentsAsync(CommentFilteringParams commentFilteringParams ,PaginationParams paginationParams);
        Task<CommentDto> GetCommentByIdAsync(int commentId);
        Task<CommentDto> AddCommentAsync(AddCommentDto addCommentDto , string userId);
        Task<CommentDto> UpdateCommentAsync(UpdateCommentDto updateCommentDto , int commentId , string userId);
        Task DeleteCommentAsync(int commentId , string userId);
    }
}