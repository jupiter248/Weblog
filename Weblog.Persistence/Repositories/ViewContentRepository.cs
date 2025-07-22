using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Weblog.Application.Interfaces.Repositories;
using Weblog.Domain.Enums;
using Weblog.Domain.JoinModels;
using Weblog.Domain.Models;
using Weblog.Persistence.Data;

namespace Weblog.Persistence.Repositories
{
    public class ViewContentRepository : IViewContentRepository
    {
        private readonly ApplicationDbContext _context;
        public ViewContentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ViewContent>> GetAllContentViewersAsync(int entityTypeId, LikeAndViewType entityType)
        {
            return await _context.ViewContents.Where(l => l.EntityId == entityTypeId && l.EntityType == entityType).Include(a => a.AppUser).ToListAsync();
        }

        public async Task<int> GetViewCountAsync(int entityTypeId, LikeAndViewType entityType)
        {
            return await _context.ViewContents.Where(e => e.EntityId == entityTypeId && e.EntityType == entityType).CountAsync();
        }

        public async Task<bool> IsViewedAsync(string userId, int entityTypeId, LikeAndViewType entityType)
        {
            ViewContent? viewContent = await _context.ViewContents.FirstOrDefaultAsync(v => v.UserId == userId && v.EntityId == entityTypeId && v.EntityType == entityType);
            if (viewContent == null)
            {
                return false;
            }
            return true;
        }

        public async Task ViewAsync(AppUser appUser, int entityTypeId, LikeAndViewType entityType)
        {
            ViewContent viewContent = new ViewContent
            {
                UserId = appUser.Id,
                AppUser = appUser,
                EntityId = entityTypeId,
                EntityType = entityType
            };
            await _context.ViewContents.AddAsync(viewContent);
            await _context.SaveChangesAsync();
        }
    }
}