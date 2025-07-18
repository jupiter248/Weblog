using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Weblog.Application.Interfaces.Repositories;
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

        public async Task<bool> EventAddedToFavoriteAsync(FavoriteEvent favoriteEvent)
        {
            FavoriteEvent? favoriteEventExists = await _context.FavoriteEvents.FirstOrDefaultAsync(f => f.UserId == favoriteEvent.UserId && f.EventId == favoriteEvent.EventId);
            if (favoriteEventExists == null)
            {
                return false;
            }
            return true;
        }

        public async Task<List<FavoriteEvent>> GetAllFavoriteEventsAsync(string userId)
        {
            return await _context.FavoriteEvents.Include(a => a.Event).Where(a => a.UserId == userId).ToListAsync();
        }

        public async Task<FavoriteEvent?> GetFavoriteEventByIdAsync(int id)
        {
            FavoriteEvent? favoriteEvent = await _context.FavoriteEvents.FirstOrDefaultAsync(f => f.Id == id);
            if (favoriteEvent == null)
            {
                return null;
            }
            return favoriteEvent;
        }
    }
}