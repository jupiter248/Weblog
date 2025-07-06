using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Weblog.Application.CustomExceptions;
using Weblog.Application.Dtos.AuthDtos;
using Weblog.Application.Dtos.SmsDtos;
using Weblog.Application.Interfaces.Services;
using Weblog.Infrastructure.Identity;
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

            if (!verified)
            {
                throw new ValidationException("The code is incorrect");
            }

            AppUser? user = await _userManager.FindByLoginAsync("Phone",loginDto.PhoneNumber) ?? throw new ValidationException("User not found");                

            var role = await _userManager.GetRolesAsync(user);

            return new AuthResponseDto
            {
                PhoneNumber = user.PhoneNumber ?? string.Empty,
                Token = JwtTokenService.CreateToken(user, role),
                FirstName = user.FirstName,
                LastName = user.LastName,
                FullName = user.FullName
            };
        }

        public async Task RegisterAsync(RegisterDto registerDto)
        {
            bool verified = await _smsService.VerifyConsentSmsAsync(new VerifyConsentSms
            {
                Code = registerDto.Code,
                Mobile = registerDto.PhoneNumber,
                Purpose = "register"
            });

            if (!verified)
            {
                throw new ValidationException("The code is incorrect");
            }
            
            AppUser appUser = new AppUser
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                FullName = $"{registerDto.FirstName} {registerDto.LastName}"
            };

            AppUser? user = await _userManager.FindByLoginAsync("Phone", registerDto.PhoneNumber) ?? throw new ValidationException("User not found");                
            if (user != null)
            {
                throw new ConflictException("Phone number already used");
            }
            var createdUser = await _userManager.CreateAsync(appUser);
            if (createdUser.Succeeded)
            {
                var roleResult = await _userManager.AddToRoleAsync(appUser, "User");
                if (!roleResult.Succeeded)
                {
                    throw new ValidationException("The role could not have added");
                }
            }
            else
            {
                throw new ValidationException("The user could not have created");
            }

        }
    }
}