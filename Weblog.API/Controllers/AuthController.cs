using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Weblog.Application.Dtos.AuthDtos;
using Weblog.Application.Dtos.SmsDtos;
using Weblog.Application.Interfaces.Services;
using Weblog.Application.Validations;
using Weblog.Application.Validations.User;
using Weblog.Domain.Enums;

namespace Weblog.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ISmsService _smsService;

        public AuthController(IAuthService authService, ISmsService smsService)
        {
            _authService = authService;
            _smsService = smsService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            Validator.ValidateAndThrow(registerDto, new RegisterValidator());
            AuthResponseDto authResponseDto = await _authService.RegisterAsync(registerDto , UserType.User);
            return Ok(authResponseDto);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterDto registerDto)
        {
            Validator.ValidateAndThrow(registerDto, new RegisterValidator());
            AuthResponseDto authResponseDto = await _authService.RegisterAsync(registerDto , UserType.Admin);
            return Ok(authResponseDto);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            Validator.ValidateAndThrow(loginDto, new LoginValidator());
            AuthResponseDto authResponseDto = await _authService.LoginAsync(loginDto);
            return Ok(authResponseDto);
        }
    }
}