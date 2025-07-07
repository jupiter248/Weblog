using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Weblog.Application.CustomExceptions;
using Weblog.Application.Dtos.AuthDtos;
using Weblog.Application.Dtos.SmsDtos;
using Weblog.Application.Interfaces.Services;
using Weblog.Domain.Models;
using Weblog.Persistence.Services.Generators;

namespace Weblog.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ISmsService _smsService;

        public AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ISmsService smsService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _smsService = smsService;
        }
        public async Task<AuthResponseDto> LoginAsync(LoginDto loginDto)
        {
            bool verified = await _smsService.VerifyConsentSmsAsync(new VerifyConsentSms
            {
                Code = loginDto.Code,
                Mobile = loginDto.PhoneNumber,
                Purpose = "login"
            });

            // if (!verified)
            // {
            //     throw new ValidationException("The code is incorrect");
            // }

            AppUser? user = await _userManager.FindByLoginAsync("Phone", loginDto.PhoneNumber);
            if (user == null)
            {
                user = await _userManager.FindByNameAsync(loginDto.Username) ?? throw new NotFoundException("User not found");
            }                

            var role = await _userManager.GetRolesAsync(user);

            return new AuthResponseDto
            {
                Username = user.UserName ?? string.Empty,
                PhoneNumber = user.PhoneNumber ?? string.Empty,
                Token = JwtTokenService.CreateToken(user, role),
                FirstName = user.FirstName,
                LastName = user.LastName,
                FullName = user.FullName,
            };
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto)
        {
            bool verified = await _smsService.VerifyConsentSmsAsync(new VerifyConsentSms
            {
                Code = registerDto.Code,
                Mobile = registerDto.PhoneNumber,
                Purpose = "register"
            });

            // if (!verified)
            // {
            //     throw new ValidationException("The code is incorrect");
            // }
            AppUser? user = await _userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == registerDto.PhoneNumber);       
            if (user != null)
            {
                throw new ConflictException("Phone number already used");
            }
            user = await _userManager.FindByNameAsync(registerDto.Username);
            if (user != null)
            {
                throw new ConflictException("Username already used");
            }
            
            AppUser appUser = new AppUser
            {
                PhoneNumber = registerDto.PhoneNumber,
                UserName = registerDto.Username,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                FullName = $"{registerDto.FirstName} {registerDto.LastName}"
            };

            var createdUser = await _userManager.CreateAsync(appUser);
            if (createdUser.Succeeded)
            {
                var roleResult = await _userManager.AddToRoleAsync(appUser, "User");
                if (!roleResult.Succeeded)
                {
                    throw new ValidationException("The role could not have added");
                }
                var role = await _userManager.GetRolesAsync(appUser);
                return new AuthResponseDto
                {
                    Username = appUser.UserName ?? string.Empty,
                    PhoneNumber = appUser.PhoneNumber ?? string.Empty,
                    Token = JwtTokenService.CreateToken(appUser , role),
                    FirstName = appUser.FirstName,
                    LastName = appUser.LastName,
                    FullName = appUser.FullName
                };
            }
            else
            {
                throw new Exception();
            }

        }
    }
}