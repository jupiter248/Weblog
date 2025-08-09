using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Weblog.Application.Interfaces.Repositories;
using Weblog.Application.Queries;
using Weblog.Application.Queries.FilteringParams;
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

        public async Task AddContributorAsync(Event eventModel, Contributor contributor)
        {
            eventModel.Contributors.Add(contributor);
            await _context.SaveChangesAsync();
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

        public async Task DeleteContributorAsync(Event eventModel, Contributor contributor)
        {
            eventModel.Contributors.Remove(contributor);
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

        public async Task<bool> EventExistsAsync(int eventId)
        {
            Event? eventModel = await _context.Events.FirstOrDefaultAsync(a => a.Id == eventId);
            if (eventModel == null)
            {
                return false;
            }
            return true;
        }

        public async Task<List<Event>> GetAllEventsAsync(EventFilteringParams eventFilteringParams, PaginationParams paginationParams)
        {
            var eventQuery = _context.Events.Include(c => c.Contributors).Include(c => c.Category).Include(t => t.Tags).AsQueryable();

            if (eventFilteringParams.CategoryId.HasValue)
            {
                eventQuery = eventQuery.Where(a => a.CategoryId == eventFilteringParams.CategoryId);
            }
            if (!string.IsNullOrWhiteSpace(eventFilteringParams.Place))
            {
                eventQuery = eventQuery.Where(a => a.Place.ToLower() == eventFilteringParams.Place.ToLower());
            }

            if (eventFilteringParams.NewestArrivals == true)
            {
                eventQuery = eventQuery.OrderByDescending(p => p.CreatedAt);
            }
            else
            {
                eventQuery = eventQuery.OrderBy(p => p.CreatedAt);
            }
            
            if (eventFilteringParams.IsPublished)
            {
                eventQuery = eventQuery.Where(a => a.IsDisplayed == true);
            }
            else if(eventFilteringParams.IsPublished == false)
            {
                eventQuery = eventQuery.Where(a => a.IsDisplayed == false);
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

        public async Task<List<Event>> GetSuggestionsAsync(PaginationParams paginationParams, Event eventModel)
        {
            List<int> tagIds = eventModel.Tags.Select(t => t.Id).ToList();
            List<int> contributorIds = eventModel.Contributors.Select(t => t.Id).ToList();
            int categoryId = eventModel.CategoryId;

            var skipNumber = (paginationParams.PageNumber - 1) * paginationParams.PageSize;

            var query = _context.Events
                .Where(a => a.Id != eventModel.Id && (a.CategoryId == categoryId || a.Tags.Any(t => tagIds.Contains(t.Id) ||
                        a.Contributors.Any(c => contributorIds.Contains(c.Id)) || a.Contributors.Any(c => contributorIds.Contains(c.Id)))))
                        .OrderByDescending(a => a.DisplayedAt)
                        .Take(10);

            return await query.Skip(skipNumber).Take(paginationParams.PageSize).ToListAsync();
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

    }
}