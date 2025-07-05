using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Weblog.Application.CustomExceptions;
using Weblog.Application.Dtos.SmsDtos;
using Weblog.Application.Interfaces.Services;

namespace Weblog.Infrastructure.Services
{
    public class SmsService : ISmsService
    {
        public async Task<HttpResponseMessage> SendConsentSms(AddSendModelDto addSendModelDto)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("x-api-key", Environment.GetEnvironmentVariable("SMS_ApiKey"));

            VerifySendModel model = new VerifySendModel()
            {
                Mobile = addSendModelDto.Mobile,
                TemplateId = int.Parse(Environment.GetEnvironmentVariable("SMS_TemplateId") ?? throw new ValidationException("TemplateId is invalid")),
                Parameters =
                [
                    new VerifySendParameterModel { Name = "Name" , Value = addSendModelDto.Name},
                    new VerifySendParameterModel { Name = "Code" , Value = addSendModelDto.Code}
                ]
            };

            string payload = JsonSerializer.Serialize(model);
            StringContent stringContent = new(payload, Encoding.UTF8, "application/json");
            return await httpClient.PostAsync(Environment.GetEnvironmentVariable("Sms_Server"), stringContent);
        }
    }
}