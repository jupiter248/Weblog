using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblog.Application.Dtos.AuthDtos;
using Weblog.Domain.Enums;

namespace Weblog.Application.Interfaces.Services
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto , UserType userType);
        Task<AuthResponseDto> LoginAsync(LoginDto loginDto);
    }
}