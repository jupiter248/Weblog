using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Weblog.Application.Interfaces.Repositories;
using Weblog.Application.Queries;
using Weblog.Domain.Enums;
using Weblog.Domain.Models;
using Weblog.Persistence.Data;

namespace Weblog.Persistence.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly ApplicationDbContext _context;
        public EventRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Event> AddEventAsync(Event eventModel)
        {
            await _context.Events.AddAsync(eventModel);
            await _context.SaveChangesAsync();
            return eventModel;  
        }

        public async Task AddTagToEvent(Event eventModel, Tag tag)
        {
            eventModel.Tags.Add(tag);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEventAsync(Event eventModel)
        {
            _context.Events.Remove(eventModel);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTagFromEvent(Event eventModel, Tag tag)
        {
            eventModel.Tags.Remove(tag);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Event>> GetAllEventsAsync(FilteringParams filteringParams, PaginationParams paginationParams)
        {
            var eventQuery = _context.Events.Include(t => t.Tags).AsQueryable();

            if (filteringParams.CategoryId.HasValue)
            {
                eventQuery = eventQuery.Where(a => a.CategoryId == filteringParams.CategoryId);
            }

            var events = await eventQuery.ToListAsync();
            foreach (var eventModel in events)
            {
                eventModel.Media = await _context.Media
                    .Where(m => m.EntityId == eventModel.Id && m.EntityType == EntityType.Event)
                    .ToListAsync();
            }
            var skipNumber = (paginationParams.PageNumber - 1) * paginationParams.PageSize;

            return events.Skip(skipNumber).Take(paginationParams.PageSize).ToList();
        }

        public async Task<Event?> GetEventByIdAsync(int eventId)
        {
            Event? eventModel = await _context.Events.Include(m => m.Media.Where(m => m.EntityType == EntityType.Event)).Include(t => t.Tags).FirstOrDefaultAsync(e => e.Id == eventId);
            if (eventModel == null)
            {
                return null;
            }
            eventModel.Media = await _context.Media
                .Where(m => m.EntityId == eventModel.Id && m.EntityType == EntityType.Event)
                .ToListAsync();
            return eventModel;
        }

        public async Task<List<Event>> SearchByTitleAsync(string keyword)
        {
            return await _context.Events
            .Where(p => p.Title.ToLower().Contains(keyword))
            .ToListAsync();
        }

        public async Task UpdateEventAsync(Event eventModel)
        {
            _context.Events.Update(eventModel);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateLikesAsync(Event eventModel)
        {
            eventModel.Likes++;
            await _context.SaveChangesAsync();
        }

        public async Task UpdateViewersAsync(Event eventModel)
        {
            eventModel.Viewers++;
            await _context.SaveChangesAsync();
        }
    }
}