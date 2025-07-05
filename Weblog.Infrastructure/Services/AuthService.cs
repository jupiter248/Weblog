using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Weblog.Application.CustomExceptions;
using Weblog.Application.Dtos.AuthDtos;
using Weblog.Application.Interfaces.Services;
using Weblog.Infrastructure.Identity;
using Weblog.Persistence.Services.Generators;

namespace Weblog.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        public AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        public async Task<AuthResponseDto> LoginAsync(LoginDto loginDto)
        {
            AppUser? user = await _userManager.FindByNameAsync(loginDto.UsernameOrEmail);
            if (user == null)
                user = await _userManager.FindByEmailAsync(loginDto.UsernameOrEmail) ?? throw new ValidationException("User not found");
                

            var result = await _signInManager.PasswordSignInAsync(user, loginDto.Password, loginDto.RememberMe, true);

            if (result.Succeeded == false)
                throw new ValidationException("Incorrect Password");

            var role = await _userManager.GetRolesAsync(user);

            return new AuthResponseDto
            {
                Username = user.UserName ?? string.Empty,
                PhoneNumber = user.PhoneNumber ?? string.Empty,
                Email = user.Email,
                Token = JwtTokenService.CreateToken(user, role),
                FirstName = user.FirstName,
                LastName = user.LastName,
                FullName = user.FullName
            };
        }

        public async Task RegisterAsync(RegisterDto registerDto)
        {
            AppUser appUser = new AppUser
            {
                UserName = registerDto.Username,
                Email = registerDto.Email,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                FullName = $"{registerDto.FirstName} {registerDto.LastName}"
            };

            AppUser? userExistence = await _userManager.FindByNameAsync(appUser.UserName);
            if (userExistence != null)
            {
                throw new ConflictException("duplicate username");
            }
            var createdUser = await _userManager.CreateAsync(appUser, registerDto.Password);
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