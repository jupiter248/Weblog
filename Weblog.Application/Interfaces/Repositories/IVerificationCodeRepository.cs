using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblog.Domain.Models;

namespace Weblog.Application.Interfaces.Repositories
{
    public interface IVerificationCodeRepository
    {
        Task AddVerificationCodeAsync(VerificationCode verificationCode);
        Task<VerificationCode?> GetVerificationCode(string phone, string code, string purpose);
        Task DeleteVerificationCodeAsync(VerificationCode verificationCode);
    }
}