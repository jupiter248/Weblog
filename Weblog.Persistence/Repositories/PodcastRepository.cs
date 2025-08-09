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

        public async Task<List<Podcast>> GetAllPodcastsAsync(PodcastFilteringParams podcastFilteringParams, PaginationParams paginationParams)
        {
            var podcastQuery = _context.Podcasts.Include(c => c.Category).Include(t => t.Tags).Include(c => c.Contributors).AsQueryable();

            if (podcastFilteringParams.CategoryId.HasValue)
            {
                podcastQuery = podcastQuery.Where(a => a.CategoryId == podcastFilteringParams.CategoryId);
            }

            if (podcastFilteringParams.NewestArrivals == true)
            {
                podcastQuery = podcastQuery.OrderByDescending(p => p.CreatedAt);
            }
            else
            {
                podcastQuery = podcastQuery.OrderBy(p => p.CreatedAt);
            }

            if (podcastFilteringParams.IsPublished == true)
            {
                podcastQuery = podcastQuery.Where(a => a.IsDisplayed == true);
            }
            else if (podcastFilteringParams.IsPublished == false)
            {
                podcastQuery = podcastQuery.Where(a => a.IsDisplayed == false);
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

        public async Task<List<Podcast>> GetSuggestionsAsync(PaginationParams paginationParams, Podcast podcast)
        {
            List<int> tagIds = podcast.Tags.Select(t => t.Id).ToList();
            List<int> contributorIds = podcast.Contributors.Select(t => t.Id).ToList();
            int categoryId = podcast.CategoryId;

            var skipNumber = (paginationParams.PageNumber - 1) * paginationParams.PageSize;

            var query = _context.Podcasts
                .Where(a => a.Id != podcast.Id && (a.CategoryId == categoryId || a.Tags.Any(t => tagIds.Contains(t.Id) ||
                        a.Contributors.Any(c => contributorIds.Contains(c.Id)) || a.Contributors.Any(c => contributorIds.Contains(c.Id)))))
                        .OrderByDescending(a => a.DisplayedAt)
                        .Take(10);

            return await query.Skip(skipNumber).Take(paginationParams.PageSize).ToListAsync();        }

        public async Task<bool> PodcastExistsAsync(int podcastId)
        {
            Podcast? podcast = await _context.Podcasts.FirstOrDefaultAsync(a => a.Id == podcastId);
            if (podcast == null)
            {
                return false;
            }
            return true; 
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