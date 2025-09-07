using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Weblog.Application.Dtos.LikeContentDtos;
using Weblog.Application.Interfaces.Repositories;
using Weblog.Domain.Enums;
using Weblog.Domain.JoinModels;
using Weblog.Domain.Models;
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

        public async Task<List<LikeContent>> GetAllContentLikesAsync(int entityTypeId, LikeAndViewType entityType)
        {
            return await _context.LikeContents.Where(l => l.EntityId == entityTypeId && l.EntityType == entityType).Include(a => a.AppUser).ToListAsync();
        }

        public async Task<List<LikeContent>> GetAllLikeContentAsync(string UserId)
        {
            return await _context.LikeContents.Where(u => u.UserId == UserId).ToListAsync();
        }

        public async Task<int> GetLikeCountAsync(int entityTypeId, LikeAndViewType entityType)
        {
            return await _context.LikeContents.Where(e => e.EntityId == entityTypeId && e.EntityType == entityType).CountAsync();
        }

        public async Task<bool> IsLikedAsync(string userId, int entityTypeId, LikeAndViewType entityType)
        {
            LikeContent? likeContents = await _context.LikeContents.Where(l => l.UserId == userId && l.EntityId == entityTypeId && l.EntityType == entityType).FirstOrDefaultAsync();
            if (likeContents == null)
            {
                return false;
            }
            return true;
        }

        public async Task LikeAsync(AppUser appUser, LikeContentDto likeContentDto)
        {
            LikeContent likeContent = new LikeContent()
            {
                UserId = appUser.Id,
                AppUser = appUser,
                EntityId = likeContentDto.EntityTypeId,
                EntityType = likeContentDto.EntityType,
                LikedOn = DateTimeOffset.Now
            };
            await _context.LikeContents.AddAsync(likeContent);
            await _context.SaveChangesAsync();
        }

        public async Task UnlikeAsync(string userId , UnLikeContentDto unLikeContentDto)
        {
            LikeContent? likeContent = await _context.LikeContents.Where(l => l.UserId == userId && l.EntityId == unLikeContentDto.EntityTypeId && l.EntityType == unLikeContentDto.EntityType).FirstOrDefaultAsync();
            if (likeContent != null)
            {
                _context.LikeContents.Remove(likeContent);
                await _context.SaveChangesAsync();
            }
        }
    }
}