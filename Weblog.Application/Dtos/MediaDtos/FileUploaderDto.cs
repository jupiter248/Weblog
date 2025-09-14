using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Weblog.Domain.Enums;

namespace Weblog.Application.Dtos.MediaDtos
{
    public class FileUploaderDto
    {
        public required IFormFile UploadedFile { get; set; }
        public MediumType MediumType { get; set; }

    }
}