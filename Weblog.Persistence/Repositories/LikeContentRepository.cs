using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Weblog.Application.Interfaces.Repositories;
using Weblog.Domain.Enums;
using Weblog.Domain.JoinModels;
using Weblog.Persistence.Data;

namespace Weblog.Persistence.Repositories
{
    public class LikeContentRepository : ILikeContentRepository
    {
        private readonly ApplicationDbContext _context;
        public LikeContentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<LikeContent> AddLikeContentAsync(LikeContent likeContent)
        {
            await _context.LikeContents.AddAsync(likeContent);
            await _context.SaveChangesAsync();
            return likeContent;
        }

        public async Task DeleteLikeContentAsync(LikeContent likeContent)
        {
            _context.LikeContents.Remove(likeContent);
            await _context.SaveChangesAsync();
        }

        public async Task<List<LikeContent>> GetAllContentLikesAsync(int entityTypeId, LikeAndViewType entityType)
        {
            return await _context.LikeContents.Where(l => l.EntityId == entityTypeId && l.EntityType == entityType).Include(a => a.AppUser).ToListAsync();
        }

        public async Task<List<LikeContent>> GetAllLikeContentAsync(string UserId)
        {
            return await _context.LikeContents.Where(u => u.UserId == UserId).ToListAsync();
        }

        public async Task<LikeContent?> GetLikeContentByIdAsync(int likeContentId)
        {
            LikeContent? likeContent = await _context.LikeContents.FirstOrDefaultAsync(l => l.Id == likeContentId);
            if (likeContent == null)
            {
                return null;
            }
            return likeContent;
        }

        public async Task<int> GetLikeCountAsync(int entityTypeId, LikeAndViewType entityType)
        {
            return await _context.LikeContents.Where(e => e.EntityId == entityTypeId && e.EntityType == entityType).CountAsync();
        }
    }
}