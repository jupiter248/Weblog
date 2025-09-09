using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Weblog.Application.Interfaces.Repositories;
using Weblog.Application.Queries;
using Weblog.Application.Queries.FilteringParams;
using Weblog.Domain.JoinModels;
using Weblog.Domain.Models;
using Weblog.Persistence.Data;

namespace Weblog.Persistence.Repositories
{
    public class FavoriteEventRepository : IFavoriteEventRepository
    {
        private readonly ApplicationDbContext _context;
        public FavoriteEventRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddEventToFavoriteAsync(FavoriteEvent favoriteEvent)
        {
            await _context.FavoriteEvents.AddAsync(favoriteEvent);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEventFromFavoriteAsync(FavoriteEvent favoriteEvent)
        {
            _context.FavoriteEvents.Remove(favoriteEvent);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsEventFavoriteAsync(FavoriteEvent favoriteEvent)
        {
            FavoriteEvent? favoriteEventExists = await _context.FavoriteEvents.FirstOrDefaultAsync(f => f.UserId == favoriteEvent.UserId && f.EventId == favoriteEvent.EventId);
            if (favoriteEventExists == null)
            {
                return false;
            }
            return true;
        }

        public async Task<List<FavoriteEvent>> GetAllFavoriteEventsAsync(string userId, FavoriteFilteringParams favoriteFilteringParams, PaginationParams paginationParams)
        {
            var favoriteEventQuery = _context.FavoriteEvents.Include(a => a.Event).ThenInclude(m => m.Media).Where(a => a.UserId == userId).AsQueryable();
            if (favoriteFilteringParams.FavoriteListId.HasValue)
            {
                favoriteEventQuery = favoriteEventQuery.Where(f => f.FavoriteListId == favoriteFilteringParams.FavoriteListId);
            }

            if (favoriteFilteringParams.NewestArrivals == true)
            {
                favoriteEventQuery = favoriteEventQuery.OrderByDescending(a => a.AddedAt);
            }
            else
            {
                favoriteEventQuery = favoriteEventQuery.OrderBy(a => a.AddedAt);
            }

            List<FavoriteEvent> favoriteEvents = await favoriteEventQuery.ToListAsync();
            var skipNumber = (paginationParams.PageNumber - 1) * paginationParams.PageSize;

            return favoriteEventQuery.Skip(skipNumber).Take(paginationParams.PageSize).ToList(); 
       }

        public async Task<FavoriteEvent?> GetFavoriteEventByIdAsync(int eventId)
        {
            FavoriteEvent? favoriteEvent = await _context.FavoriteEvents.Where(e => e.EventId == eventId).FirstOrDefaultAsync();
            if (favoriteEvent == null)
            {
                return null;
            }
            return favoriteEvent;
        }
    }
}