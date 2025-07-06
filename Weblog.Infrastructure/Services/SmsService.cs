using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Weblog.Application.CustomExceptions;
using Weblog.Application.Dtos.SmsDtos;
using Weblog.Application.Interfaces.Repositories;
using Weblog.Application.Interfaces.Services;
using Weblog.Domain.Models;
using Weblog.Infrastructure.Helpers;
using Weblog.Infrastructure.Identity;

namespace Weblog.Infrastructure.Services
{
    public class SmsService : ISmsService
    {
        private readonly IVerificationCodeRepository _verificationCodeRepo;
        private readonly UserManager<AppUser> _userManager;

        public SmsService(IVerificationCodeRepository verificationCodeRepo, UserManager<AppUser> userManager)
        {
            _verificationCodeRepo = verificationCodeRepo;
            _userManager = userManager;
        }
        public async Task SendConsentSmsAsync(AddConsentSmsDto addConsentSmsDto)
        {
            if (addConsentSmsDto.Purpose.ToLower() == "register")
            {
                AppUser? user = await _userManager.FindByLoginAsync("Phone", addConsentSmsDto.Mobile);
                if (user != null)
                {
                    throw new ConflictException("This phone already registered");
                }            

            }
            string code = CodeGenerator.GenerateConsentCode();
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("x-api-key", Environment.GetEnvironmentVariable("SMS_ApiKey"));

            ConsentSmsModel model = new ConsentSmsModel()
            {
                Mobile = addConsentSmsDto.Mobile,
                TemplateId = int.Parse(Environment.GetEnvironmentVariable("SMS_TemplateId") ?? throw new ValidationException("TemplateId is invalid")),
                Parameters =
                [
                    new ConsentSmsParameterModel { Name = "Code" , Value = code}
                ]
            };
            string payload = JsonSerializer.Serialize(model);
            StringContent stringContent = new(payload, Encoding.UTF8, "application/json");

            HttpResponseMessage httpResponseMessage = await httpClient.PostAsync(Environment.GetEnvironmentVariable("Sms_Server"), stringContent);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                VerificationCode verificationCode = new VerificationCode
                {
                    Code = code,
                    ExpiresAt = DateTimeOffset.UtcNow.AddMinutes(3),
                    Phone = addConsentSmsDto.Mobile,
                    Purpose = addConsentSmsDto.Purpose
                };
                await _verificationCodeRepo.AddVerificationCodeAsync(verificationCode);
            }
        }

        public async Task<bool> VerifyConsentSmsAsync(VerifyConsentSms verifyConsentSms)
        {
            VerificationCode? verificationCode = await _verificationCodeRepo.GetVerificationCode(verifyConsentSms.Mobile, verifyConsentSms.Code, verifyConsentSms.Purpose);
            if (verificationCode == null)
            {
                return false;
            }
            await _verificationCodeRepo.DeleteVerificationCodeAsync(verificationCode);
            return true;
        }
    }
}