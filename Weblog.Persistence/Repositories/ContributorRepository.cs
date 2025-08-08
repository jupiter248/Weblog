using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Weblog.Application.Interfaces.Repositories;
using Weblog.Domain.Enums;
using Weblog.Domain.Models;
using Weblog.Persistence.Data;

namespace Weblog.Persistence.Repositories
{
    public class ContributorRepository : IContributorRepository
    {
        private readonly ApplicationDbContext _context;
        public ContributorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Contributor> AddContributorAsync(Contributor contributor)
        {
            await _context.Contributors.AddAsync(contributor);
            await _context.SaveChangesAsync();
            return contributor;
        }

        public async Task DeleteContributorAsync(Contributor contributor)
        {
            _context.Contributors.Remove(contributor);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Contributor>> GetAllContributorsAsync()
        {
            List<Contributor> contributors = await _context.Contributors.ToListAsync();
            foreach (var contributor in contributors)
            {
                contributor.Media = await _context.Media
                    .Where(m => m.EntityId == contributor.Id && m.EntityType == EntityType.Contributor)
                    .ToListAsync();
            }
            return contributors;
        }

        public async Task<Contributor?> GetContributorByIdAsync(int contributorId)
        {
            Contributor? contributor = await _context.Contributors.FirstOrDefaultAsync(t => t.Id == contributorId) ?? null;
            if (contributor == null)
            {
                return null;
            }
            contributor.Media = await _context.Media
                .Where(m => m.EntityId == contributor.Id && m.EntityType == EntityType.Contributor)
                .ToListAsync();
            return contributor;
        }

        public async Task UpdateContributorAsync(Contributor currentContributor, Contributor newContributor)
        {
            currentContributor.FirstName = newContributor.FirstName;
            currentContributor.FamilyName = newContributor.FamilyName;
            currentContributor.Description = newContributor.Description;
            currentContributor.FullName = $"{newContributor.FirstName} {newContributor.FamilyName}";
            await _context.SaveChangesAsync();
        }
        public async Task<List<Contributor>> SearchByNameAsync(string keyword)
        {
            return await _context.Contributors
            .Where(a => a.FullName.ToLower().Contains(keyword))
            .ToListAsync();
        }
    }
}