using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Weblog.Application.Interfaces.Repositories;
using Weblog.Domain.JoinModels;
using Weblog.Persistence.Data;

namespace Weblog.Persistence.Repositories
{
    public class TakingPartRepository : ITakingPartRepository
    {
        private readonly ApplicationDbContext _context;
        public TakingPartRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task CancelTakingPartAsync(TakingPart takingPart)
        {
            _context.TakingParts.Remove(takingPart);
            await _context.SaveChangesAsync();        }

        public async Task<List<TakingPart>> GetAllTakingPartsByEventIdAsync( int eventId)
        {
            return await _context.TakingParts.Include(t => t.AppUser).Include(t => t.Event).Where(t => t.EventId == eventId).ToListAsync();
        }

        public async Task<TakingPart?> GetTakingPartByIdAsync(int id)
        {
            TakingPart? takingPart = await _context.TakingParts.FirstOrDefaultAsync(t => t.Id == id);
            if (takingPart == null)
            {
                return null;
            }
            return takingPart;
        }

        public async Task TakePartAsync(TakingPart takingPart)
        {
            await _context.TakingParts.AddAsync(takingPart);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsUserParticipant(TakingPart takingPart)
        {
            TakingPart? takingPartExists = await _context.TakingParts.FirstOrDefaultAsync(t => t.UserId == takingPart.UserId && t.EventId == takingPart.EventId);
            if (takingPartExists == null)
            {
                return false;
            }
            return true;
        }

        public async Task UpdateTakingPartAsync(TakingPart takingPart)
        {
            _context.TakingParts.Update(takingPart);
            await _context.SaveChangesAsync();
        }
    }
}