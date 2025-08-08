using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Weblog.Application.Dtos.AuthDtos;
using Weblog.Application.Dtos.SmsDtos;
using Weblog.Application.Interfaces.Services;

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
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            AuthResponseDto authResponseDto = await _authService.RegisterAsync(registerDto);
            return Ok(authResponseDto);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            AuthResponseDto authResponseDto = await _authService.LoginAsync(loginDto);
            return Ok(authResponseDto);
        }
        // [HttpPost("consent-code")]
        // public async Task<IActionResult> SendConsentCode([FromBody] AddConsentSmsDto addConsentSmsDto)
        // {
        //     await _smsService.SendConsentSmsAsync(addConsentSmsDto);
        //     return Ok();
        // }
    }
}