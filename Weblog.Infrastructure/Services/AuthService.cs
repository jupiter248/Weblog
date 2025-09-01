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
using Weblog.Domain.Enums;
using Weblog.Domain.Errors.Common;
using Weblog.Domain.Errors.User;
using Weblog.Domain.Models;
using Weblog.Infrastructure.Services.Generators;

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
            AppUser? user = await _userManager.FindByNameAsync(loginDto.Username ?? throw new UnauthorizedException(CommonErrorCodes.Unauthorized , [UserErrorCodes.InvalidUsername]));
            if (user == null)
            {
                throw new NotFoundException(UserErrorCodes.UserNotFound);
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password ?? throw new UnauthorizedException(CommonErrorCodes.Unauthorized , [UserErrorCodes.IncorrectPassword]), false);
            if (!result.Succeeded)
            {
                throw new UnauthorizedException(CommonErrorCodes.Unauthorized, [UserErrorCodes.IncorrectPassword]);
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

        public async Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto , UserType userType)
        {
            AppUser? user = await _userManager.FindByNameAsync(registerDto.Username ?? throw new UnauthorizedException(CommonErrorCodes.Unauthorized , [UserErrorCodes.InvalidUsername]));
            if (user != null)
            {
                throw new ConflictException("Username already used");
            }
            AppUser? checkingPhone = await _userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == registerDto.PhoneNumber);
            if (checkingPhone != null)
            {
                throw new ConflictException("Phone already used");
            }
            AppUser appUser = new AppUser
            {
                UserName = registerDto.Username,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                FullName = $"{registerDto.FirstName} {registerDto.LastName}",
                CreatedAt = DateTimeOffset.Now
            };
            var createdUser = await _userManager.CreateAsync(appUser, registerDto.Password);
            if (createdUser.Succeeded)
            {
                var roleResult = new IdentityResult();
                if (userType == UserType.Admin)
                {
                    roleResult = await _userManager.AddToRoleAsync(appUser, "Admin");
                }
                else if (userType == UserType.User)
                {
                    roleResult = await _userManager.AddToRoleAsync(appUser, "User");
                }
                if (!roleResult.Succeeded)
                {
                    throw new InternalServerException(CommonErrorCodes.InternalServer, [UserErrorCodes.RoleAssignFailed]);
                }
                var role = await _userManager.GetRolesAsync(appUser);
                return new AuthResponseDto
                {
                    Username = appUser.UserName ?? string.Empty,
                    PhoneNumber = appUser.PhoneNumber ?? string.Empty,
                    Token = JwtTokenService.CreateToken(appUser, role),
                    FirstName = appUser.FirstName,
                    LastName = appUser.LastName,
                    FullName = appUser.FullName,
                };
            }
            else
            {
                throw new InternalServerException(CommonErrorCodes.InternalServer, [UserErrorCodes.UserCreateFailed]);
            };
        }
    }
}