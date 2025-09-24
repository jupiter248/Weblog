using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Weblog.Application.Dtos.EventDtos;
using Weblog.Application.Interfaces.Repositories;
using Weblog.Application.Queries.FilteringParams;
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
        public async Task DenyTakingPartAsync(TakingPart takingPart)
        {
            _context.TakingParts.Remove(takingPart);
            await _context.SaveChangesAsync(); 
        }

        public async Task<List<UserTakingPart>> GetAllUserTakingPartsByEventIdAsync(int eventId, ParticipantFilteringParams participantFilteringParams)
        {
            List<UserTakingPart> takingParts = await _context.Set<UserTakingPart>().Include(t => t.AppUser).Include(t => t.Event).Where(t => t.EventId == eventId).ToListAsync();
            if (participantFilteringParams.IsConfirmed.HasValue)
            {
                if (participantFilteringParams.IsConfirmed == true)
                {
                    takingParts = takingParts.Where(t => t.IsConfirmed == true).ToList();
                }
                else if (participantFilteringParams.IsConfirmed == false)
                {
                    takingParts = takingParts.Where(t => t.IsConfirmed == false).ToList();
                }
            }
            return takingParts;
        }

        public async Task<TakingPart?> GetTakingPartByIdAsync(int id)
        {
            TakingPart? takingPart = await _context.TakingParts.Include(t => t.Event).FirstOrDefaultAsync(t => t.Id == id);
            if (takingPart == null)
            {
                return null;
            }
            return takingPart;
        }

        public async Task UserTakePartAsync(UserTakingPart takingPart)
        {
            await _context.Set<UserTakingPart>().AddAsync(takingPart);
            await _context.SaveChangesAsync();
        }
        public async Task GuestTakePartAsync(GuestTakingPart takingPart)
        {
            await _context.Set<GuestTakingPart>().AddAsync(takingPart);
            await _context.SaveChangesAsync();
        }


        public async Task<bool> IsUserParticipantAsync(UserTakingPart takingPart)
        {
            TakingPart? takingPartExists = await _context.Set<UserTakingPart>().FirstOrDefaultAsync(t => t.UserId == takingPart.UserId && t.EventId == takingPart.EventId);
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

        public async Task<List<UserTakingPart>> GetAllTookPartsByEventsAsync(string userId, int? categoryId)
        {
            var takingParts = await _context.Set<UserTakingPart>()
            .Where(u => u.UserId == userId)
            .Include(e => e.Event)
            .ToListAsync();
            if (categoryId.HasValue)
            {
                takingParts = takingParts.Where(t => t.Event.CategoryId == categoryId).ToList();
            }
            return takingParts;
        }

        public async Task<UserTakingPart?> GetTakingPartByUserIdAndEventIdAsync(string userId, int eventId)
        {
            UserTakingPart? takingPart = await _context.Set<UserTakingPart>().FirstOrDefaultAsync(t => t.EventId == eventId && t.UserId == userId);
            if (takingPart == null)
            {
                return null;
            }
            return takingPart;
        }

        public async Task<List<GuestTakingPart>> GetAllGuestTakingPartsByEventIdAsync(int eventId, ParticipantFilteringParams participantFilteringParams)
        {
            List<GuestTakingPart> takingParts = await _context.Set<GuestTakingPart>().Include(t => t.Event).Where(t => t.EventId == eventId).ToListAsync();
            if (participantFilteringParams.IsConfirmed.HasValue)
            {
                if (participantFilteringParams.IsConfirmed == true)
                {
                    takingParts = takingParts.Where(t => t.IsConfirmed == true).ToList();
                }
                else if (participantFilteringParams.IsConfirmed == false)
                {
                    takingParts = takingParts.Where(t => t.IsConfirmed == false).ToList();
                }
            }
            return takingParts; 
        }
    }
}