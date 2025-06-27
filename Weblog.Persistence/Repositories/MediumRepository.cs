using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Weblog.Application.Interfaces.Repositories;
using Weblog.Domain.Models;
using Weblog.Persistence.Data;

namespace Weblog.Persistence.Repositories
{
    public class MediumRepository : IMediumRepository
    {
        private readonly ApplicationDbContext _context;
        public MediumRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Medium> AddMediumAsync(Medium medium)
        {
            await _context.Media.AddAsync(medium);
            await _context.SaveChangesAsync();
            return medium;
        }

        public async Task DeleteMediumAsync(Medium medium)
        {
            _context.Media.Remove(medium);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Medium>> GetAllMediaAsync()
        {
            List<Medium> media = await _context.Media.ToListAsync();
            return media;
        }

        public async Task<Medium?> GetMediumByIdAsync(int mediumId)
        {
            Medium? medium = await _context.Media.FirstOrDefaultAsync(m => m.Id == mediumId);
            if (medium == null)
            {
                return null;
            }
            return medium;
        }

        public async Task UpdateMediumAsync(Medium medium)
        {
            _context.Media.Update(medium);
            await _context.SaveChangesAsync();
        }
    }
}