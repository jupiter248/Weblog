using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Weblog.Application.Interfaces.Repositories;
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
            return contributors;
        }

        public async Task<Contributor?> GetContributorByIdAsync(int contributorId)
        {
            Contributor? contributor = await _context.Contributors.FirstOrDefaultAsync(t => t.Id == contributorId) ?? null;
            return contributor;
        }

        public async Task UpdateContributorAsync(Contributor currentContributor, Contributor newContributor)
        {
            currentContributor.FirstName = newContributor.FirstName;
            currentContributor.FamilyName = newContributor.FamilyName;
            await _context.SaveChangesAsync();
        }
    }
}