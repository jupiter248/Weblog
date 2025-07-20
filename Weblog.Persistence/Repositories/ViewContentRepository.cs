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
    public class ViewContentRepository : IViewContentRepository
    {
        private readonly ApplicationDbContext _context;
        public ViewContentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ViewContent> AddViewContentAsync(ViewContent viewContent)
        {
            await _context.ViewContents.AddAsync(viewContent);
            await _context.SaveChangesAsync();
            return viewContent;
        }

        public async Task<ViewContent?> GetViewContentByIdAsync(int viewContentId)
        {
            ViewContent? viewContent = await _context.ViewContents.FirstOrDefaultAsync(l => l.Id == viewContentId);
            if (viewContent == null)
            {
                return null;
            }
            return viewContent;
        }

        public async Task<int> GetViewCountAsync(int entityTypeId, LikeAndViewType entityType)
        {
            return await _context.ViewContents.Where(e => e.EntityId == entityTypeId && e.EntityType == entityType).CountAsync();
        }
    }
}