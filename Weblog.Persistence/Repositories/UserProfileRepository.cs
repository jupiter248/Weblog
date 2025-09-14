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
    public class UserProfileRepository : IUserProfileRepository
    {
        private readonly ApplicationDbContext _context;
        public UserProfileRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddUserProfileAsync(UserProfile userProfile)
        {
            await _context.UserProfiles.AddAsync(userProfile);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserProfileAsync(UserProfile userProfile)
        {
            _context.UserProfiles.Remove(userProfile);
            await _context.SaveChangesAsync();
        }

        public async Task<List<UserProfile>> GetAllProfilesAsync()
        {
            List<UserProfile> userProfiles = await _context.UserProfiles.Include(a => a.AppUser).ToListAsync();
            return userProfiles;
        }

        public async Task<UserProfile?> GetUserProfileByIdAsync(int userProfileId)
        {
            UserProfile? userProfile = await _context.UserProfiles.Include(a => a.AppUser).FirstOrDefaultAsync(u => u.Id == userProfileId);
            if (userProfile == null)
            {
                return null;
            }
            return userProfile;
        }
    }
}