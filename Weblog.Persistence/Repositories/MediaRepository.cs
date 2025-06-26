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
    public class MediaRepository : IMediaRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public MediaRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Medium> AddMediaAsync(Medium medium)
        {
            await _context.Media.AddAsync(medium);
            await _context.SaveChangesAsync();
            return medium;
        }

        public async Task DeleteMediaAsync(Medium medium)
        {
            _context.Media.Remove(medium);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Medium>> GetAllMediumAsync()
        {
            List<Medium> media = await _context.Media.ToListAsync();
            return media;
        }

        public async Task<Medium?> GetMediaByIdAsync(int mediumId)
        {
            Medium? medium = await _context.Media.FirstOrDefaultAsync(m => m.Id == mediumId);
            if (medium == null)
            {
                return null;
            }
            return medium;
        }

        public async Task UpdateMediaAsync(Medium medium)
        {
            _context.Media.Update(medium);
            await _context.SaveChangesAsync();
        }
    }
}