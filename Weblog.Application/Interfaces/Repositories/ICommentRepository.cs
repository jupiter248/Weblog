using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblog.Application.Queries;
using Weblog.Application.Queries.FilteringParams;
using Weblog.Domain.Models;

namespace Weblog.Application.Interfaces.Repositories
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllCommentsAsync(CommentFilteringParams commentFilteringParams,PaginationParams paginationParams );
        Task<Comment> AddCommentAsync(Comment comment);
        Task<Comment?> GetCommentByIdAsync(int commentId);
        Task UpdateCommentAsync(Comment currentComment, Comment newComment);
        Task DeleteCommentAsync(Comment comment); 
    }
}