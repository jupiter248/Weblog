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
            var podcasts = _context.Podcasts.Include(m => m.Media.Where(m => m.ParentType == MediumParentType.Podcast)).Include(t => t.Tags).AsQueryable();

            if (!string.IsNullOrWhiteSpace(filteringParams.Title))
            {
                podcasts = podcasts.Where(p => p.Name.ToLower().Contains(filteringParams.Title.ToLower().Replace(" ", "")));
            }
            if (filteringParams.CategoryId.HasValue)
            {
                podcasts = podcasts.Where(a => a.CategoryId == filteringParams.CategoryId);
            }

            var skipNumber = (paginationParams.PageNumber - 1) * paginationParams.PageSize;

            return await podcasts.Skip(skipNumber).Take(paginationParams.PageSize).ToListAsync();
        }

        public async Task<Podcast?> GetPodcastByIdAsync(int podcastId)
        {
            Podcast? podcast = await _context.Podcasts.Include(m => m.Media.Where(m => m.ParentType == MediumParentType.Podcast)).Include(t => t.Tags).FirstOrDefaultAsync(p => p.Id == podcastId);
            if (podcast == null)
            {
                return null;
            }
            return podcast;
        }

        public async Task UpdatePodcastAsync(Podcast podcast)
        {
            _context.Podcasts.Update(podcast);
            await _context.SaveChangesAsync();
        }
    }
}