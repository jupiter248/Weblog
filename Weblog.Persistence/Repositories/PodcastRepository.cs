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
    public class PodcastRepository : IPodcastRepository
    {
        private readonly ApplicationDbContext _context;
        public PodcastRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddContributorAsync(Podcast podcast, Contributor contributor)
        {
            podcast.Contributors.Add(contributor);
            await _context.SaveChangesAsync();
        }

        public async Task<Podcast> AddPodcastAsync(Podcast podcast)
        {
            await _context.Podcasts.AddAsync(podcast);
            await _context.SaveChangesAsync();
            return podcast;
        }

        public async Task AddTagAsync(Podcast podcast, Tag tag)
        {
            podcast.Tags.Add(tag);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteContributorAsync(Podcast podcast, Contributor contributor)
        {
            podcast.Contributors.Remove(contributor);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePodcastAsync(Podcast podcast)
        {
            _context.Podcasts.Remove(podcast);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTagAsync(Podcast podcast, Tag tag)
        {
            podcast.Tags.Remove(tag);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Podcast>> GetAllPodcastsAsync(FilteringParams filteringParams, PaginationParams paginationParams)
        {
            var podcastQuery = _context.Podcasts.Include(c => c.Comments).Include(c => c.Category).Include(t => t.Tags).Include(c => c.Contributors).AsQueryable();

            if (filteringParams.CategoryId.HasValue)
            {
                podcastQuery = podcastQuery.Where(a => a.CategoryId == filteringParams.CategoryId);
            }

            var podcasts = await podcastQuery.ToListAsync();
            foreach (var podcast in podcasts)
            {
                podcast.Media = await _context.Media
                    .Where(m => m.EntityId == podcast.Id && m.EntityType == EntityType.Podcast)
                    .ToListAsync();
            }
            var skipNumber = (paginationParams.PageNumber - 1) * paginationParams.PageSize;

            return podcasts.Skip(skipNumber).Take(paginationParams.PageSize).ToList();
        }

        public async Task<Podcast?> GetPodcastByIdAsync(int podcastId)
        {
            Podcast? podcast = await _context.Podcasts.Include(m => m.Media.Where(m => m.EntityType == EntityType.Podcast)).Include(t => t.Tags).FirstOrDefaultAsync(p => p.Id == podcastId);
            if (podcast == null)
            {
                return null;
            }
            podcast.Media = await _context.Media
                .Where(m => m.EntityId == podcast.Id && m.EntityType == EntityType.Podcast)
                .ToListAsync();
            return podcast;
        }

        public async Task<List<Podcast>> SearchByTitleAsync(string keyword)
        {
            return await _context.Podcasts
            .Where(p => p.Name.ToLower().Contains(keyword))
            .ToListAsync();
        }

        public async Task UpdatePodcastAsync(Podcast podcast)
        {
            _context.Podcasts.Update(podcast);
            await _context.SaveChangesAsync();
        }
    }
}