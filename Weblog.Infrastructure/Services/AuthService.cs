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
            // if (loginDto.LoginAndRegisterType == LoginAndRegisterType.Phone)
            // {
            //     user = await _userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == loginDto.PhoneNumber);
            //     if (user == null)
            //     {
            //         throw new NotFoundException("User with this phone notfound");
            //     }

            //     bool verified = await _smsService.VerifyConsentSmsAsync(new VerifyConsentSms
            //     {
            //         Code = loginDto.Code ?? throw new ValidationException("Code is invalid"),
            //         Mobile = loginDto.PhoneNumber ?? throw new ValidationException("Phone is invalid"),
            //         Purpose = "login"
            //     });

            //     // if (!verified)
            //     // {
            //     //     throw new ValidationException("The code is incorrect");
            //     // }

            // }
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

        public async Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto)
        {
            // if (registerDto.LoginAndRegisterType == LoginAndRegisterType.Phone)
            // {
            //     AppUser? user = await _userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == registerDto.PhoneNumber);
            //     if (user != null)
            //     {
            //         throw new ConflictException("Phone number already used");
            //     }

            //     bool verified = await _smsService.VerifyConsentSmsAsync(new VerifyConsentSms
            //     {
            //         Code = registerDto.Code ?? throw new ValidationException("The code is invalid"),
            //         Mobile = registerDto.PhoneNumber ?? throw new ValidationException("The phone is invalid"),
            //         Purpose = "register"
            //     });

            //     // if (!verified)
            //     // {
            //     //     throw new ValidationException("The code is incorrect");
            //     // }

            //     appUser = new AppUser
            //     {
            //         PhoneNumber = registerDto.PhoneNumber,
            //         UserName = registerDto.PhoneNumber,
            //         FirstName = registerDto.FirstName,
            //         LastName = registerDto.LastName,
            //         FullName = $"{registerDto.FirstName} {registerDto.LastName}",
            //         CreatedAt = DateTimeOffset.Now
            //     };
            //     var createdUser = await _userManager.CreateAsync(appUser);
            //     if (createdUser.Succeeded)
            //     {
            //         var roleResult = await _userManager.AddToRoleAsync(appUser, "User");
            //         if (!roleResult.Succeeded)
            //         {
            //             throw new ValidationException("The role could not have added");
            //         }

            //         var role = await _userManager.GetRolesAsync(appUser);
            //         authResponseDto = new AuthResponseDto
            //         {
            //             Username = appUser.UserName ?? string.Empty,
            //             PhoneNumber = appUser.PhoneNumber ?? string.Empty,
            //             Token = JwtTokenService.CreateToken(appUser, role),
            //             FirstName = appUser.FirstName,
            //             LastName = appUser.LastName,
            //             FullName = appUser.FullName,
            //         };
            //     }
            //     else
            //     {
            //         throw new Exception();
            //     }

            // }

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
                var roleResult = await _userManager.AddToRoleAsync(appUser, "User");
                if (!roleResult.Succeeded)
                {
                    throw new InternalServerException(CommonErrorCodes.InternalServer,[UserErrorCodes.RoleAssignFailed]);
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