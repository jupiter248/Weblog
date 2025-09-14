using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Weblog.Application.CustomExceptions;
using Weblog.Application.Dtos.MediaDtos;
using Weblog.Domain.Errors.Medium;

namespace Weblog.Infrastructure.Helpers
{
    public static class Uploader
    {
        public static async Task<string> FileUploader(IWebHostEnvironment webHost, FileUploaderDto fileUploaderDto)
        {
            IFormFile mediumFile = fileUploaderDto.UploadedFile;
            if (mediumFile == null || mediumFile.Length == 0)
            {
                throw new BadRequestException(MediumErrorCodes.MediumFileInvalid);
            }
            var uploadsFolder = Path.Combine(webHost.WebRootPath, "uploads");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }
            if (!Directory.Exists(Path.Combine(webHost.WebRootPath, $"uploads/{fileUploaderDto.MediumType}")))
            {
                Directory.CreateDirectory(Path.Combine(webHost.WebRootPath, $"uploads/{fileUploaderDto.MediumType}"));
            }

            var fileName = $"{Guid.NewGuid()}-{Path.GetFileName(mediumFile.FileName)}";
            var filePath = Path.Combine(webHost.WebRootPath, $"uploads/{fileUploaderDto.MediumType}", fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await mediumFile.CopyToAsync(stream);
            }
            return fileName;
        }
    }
}