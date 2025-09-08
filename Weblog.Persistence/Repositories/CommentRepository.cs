using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Weblog.Application.Interfaces.Repositories;
using Weblog.Application.Queries;
using Weblog.Application.Queries.FilteringParams;
using Weblog.Domain.Models;
using Weblog.Persistence.Data;

namespace Weblog.Persistence.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _context;
        public CommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Comment> AddCommentAsync(Comment comment)
        {
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task DeleteCommentAsync(Comment comment)
        {
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Comment>> GetAllCommentsAsync(CommentFilteringParams commentFilteringParams , PaginationParams paginationParams)
        {
            var commentQueries = _context.Comments.Include(a => a.AppUser).AsQueryable();

            if (commentFilteringParams.EntityId.HasValue)
            {
                commentQueries = commentQueries.Where(c => c.EntityId == commentFilteringParams.EntityId && c.EntityType == commentFilteringParams.EntityType);
            }
            List<Comment> comments = await commentQueries.ToListAsync();
            var skipNumber = (paginationParams.PageNumber - 1) * paginationParams.PageSize;
            return comments.Skip(skipNumber).Take(paginationParams.PageSize).ToList();
        }

        public async Task<Comment?> GetCommentByIdAsync(int commentId)
        {
            Comment? comment = await _context.Comments.Include(a => a.AppUser).FirstOrDefaultAsync(c => c.Id == commentId);
            if (comment == null)
            {
                return null;
            }
            return comment;
        }

        public async Task UpdateCommentAsync(Comment newComment)
        {
            _context.Comments.Update(newComment);
            await _context.SaveChangesAsync();
        }
    }
}