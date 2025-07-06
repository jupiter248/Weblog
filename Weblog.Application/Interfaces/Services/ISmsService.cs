using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblog.Application.Dtos.SmsDtos;

namespace Weblog.Application.Interfaces.Services
{
    public interface ISmsService
    {
        Task SendConsentSmsAsync(AddConsentSmsDto addConsentSmsDto);
        Task<bool> VerifyConsentSmsAsync(VerifyConsentSms verifyConsentSms);
    }
}