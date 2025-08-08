using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Weblog.Domain.Enums;

namespace Weblog.Application.Dtos.AuthDtos
{
    public class LoginDto
    {
        // [Phone]
        // public required string PhoneNumber { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
        // public string? Code { get; set; }
        // public required LoginAndRegisterType LoginAndRegisterType { get; set; }
    }
}