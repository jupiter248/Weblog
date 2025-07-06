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
    public class VerificationCodeRepository : IVerificationCodeRepository
    {
        private readonly ApplicationDbContext _context;
        public VerificationCodeRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddVerificationCodeAsync(VerificationCode verificationCode)
        {
            await _context.VerificationCodes.AddAsync(verificationCode);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteVerificationCodeAsync(VerificationCode verificationCode)
        {
            _context.VerificationCodes.Remove(verificationCode);
            await _context.SaveChangesAsync();
        }

        public async Task<VerificationCode?> GetVerificationCode(string phone, string code, string purpose)
        {
            VerificationCode? record = await _context.VerificationCodes
            .FirstOrDefaultAsync(c => c.Phone == phone && c.Code == code && c.Purpose == purpose && c.ExpiresAt > DateTimeOffset.UtcNow);
            if (record == null)
            {
                return null;
            }
            return record;
        }
    }
}