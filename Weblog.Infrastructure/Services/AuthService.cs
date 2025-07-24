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
            AppUser? user = null;
            if (loginDto.LoginAndRegisterType == LoginAndRegisterType.Phone)
            {
                user = await _userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == loginDto.PhoneNumber);
                if (user == null)
                {
                    throw new NotFoundException("User with this phone notfound");
                }

                bool verified = await _smsService.VerifyConsentSmsAsync(new VerifyConsentSms
                {
                    Code = loginDto.Code ?? throw new ValidationException("Code is invalid"),
                    Mobile = loginDto.PhoneNumber ?? throw new ValidationException("Phone is invalid"),
                    Purpose = "login"
                });

                // if (!verified)
                // {
                //     throw new ValidationException("The code is incorrect");
                // }

            }
            if (loginDto.LoginAndRegisterType == LoginAndRegisterType.Username)
            {
                user = await _userManager.FindByNameAsync(loginDto.Username ?? throw new ValidationException("Username is invalid"));
                if (user == null)
                {
                    throw new NotFoundException("User with username notfound");
                }
                var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password ?? throw new ValidationException("Password is invalid"), false);
                if (!result.Succeeded)
                {
                    throw new UnauthorizedAccessException("Password does not match with the user");
                }
            }
            if (user == null)
            {
                throw new ValidationException("User is invalid");
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
            AppUser appUser;
            AuthResponseDto? authResponseDto = null;

            if (registerDto.LoginAndRegisterType == LoginAndRegisterType.Phone)
            {
                AppUser? user = await _userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == registerDto.PhoneNumber);
                if (user != null)
                {
                    throw new ConflictException("Phone number already used");
                }

                bool verified = await _smsService.VerifyConsentSmsAsync(new VerifyConsentSms
                {
                    Code = registerDto.Code ?? throw new ValidationException("The code is invalid"),
                    Mobile = registerDto.PhoneNumber ?? throw new ValidationException("The phone is invalid"),
                    Purpose = "register"
                });

                // if (!verified)
                // {
                //     throw new ValidationException("The code is incorrect");
                // }

                appUser = new AppUser
                {
                    PhoneNumber = registerDto.PhoneNumber,
                    UserName = registerDto.PhoneNumber,
                    FirstName = registerDto.FirstName,
                    LastName = registerDto.LastName,
                    FullName = $"{registerDto.FirstName} {registerDto.LastName}",
                    CreatedAt = DateTimeOffset.Now
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
                    authResponseDto = new AuthResponseDto
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
                    throw new Exception();
                }

            }

            if (registerDto.LoginAndRegisterType == LoginAndRegisterType.Username)
            {
                AppUser? user = await _userManager.FindByNameAsync(registerDto.Username ?? throw new ValidationException("The username is invalid"));
                if (user != null)
                {
                    throw new ConflictException("Username already used");
                }
                appUser = new AppUser
                {
                    UserName = registerDto.Username,
                    FirstName = registerDto.FirstName,
                    LastName = registerDto.LastName,
                    FullName = $"{registerDto.FirstName} {registerDto.LastName}",
                    CreatedAt = DateTimeOffset.Now
                };
                if (registerDto.Password != registerDto.PasswordRepetition)
                {
                    throw new ValidationException("Passwords are not the same");
                }
                var createdUser = await _userManager.CreateAsync(appUser, registerDto.Password ?? string.Empty);
                if (createdUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(appUser, "User");
                    if (!roleResult.Succeeded)
                    {
                        throw new ValidationException("The role could not have added");
                    }

                    var role = await _userManager.GetRolesAsync(appUser);
                    authResponseDto = new AuthResponseDto
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
                    throw new Exception();
                }
            }
            if (authResponseDto == null)
            {
                throw new ValidationException("Response is invalid  ");
            }
            return authResponseDto;
        }
    }
}