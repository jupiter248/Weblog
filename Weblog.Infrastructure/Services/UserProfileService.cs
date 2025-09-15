using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Weblog.Application.CustomExceptions;
using Weblog.Application.Dtos.MediaDtos;
using Weblog.Application.Dtos.UserProfileDtos;
using Weblog.Application.Interfaces.Repositories;
using Weblog.Application.Interfaces.Services;
using Weblog.Domain.Enums;
using Weblog.Domain.Errors.User;
using Weblog.Domain.Errors.UserProfile;
using Weblog.Domain.Models;
using Weblog.Infrastructure.Helpers;

namespace Weblog.Infrastructure.Services
{
    public class UserProfileService : IUserProfileService
    {
        private readonly IUserProfileRepository _userProfileRepo;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHost;

        public UserProfileService(IUserProfileRepository userProfileRepository, UserManager<AppUser> userManager,
         IMapper mapper,
         IWebHostEnvironment webHostEnvironment)
        {
            _userProfileRepo = userProfileRepository;
            _userManager = userManager;
            _mapper = mapper;
            _webHost = webHostEnvironment;
        }

        public async Task<UserProfileDto> AddUserProfileAsync(UploadUserProfileDto uploadUserProfileDto, string userId)
        {
            AppUser appUser = await _userManager.FindByIdAsync(userId) ?? throw new NotFoundException(UserErrorCodes.UserNotFound);
            List<UserProfile> userProfiles = await _userProfileRepo.GetAllProfilesAsync(); 
            if (userProfiles.Any())
            {
                foreach (UserProfile item in userProfiles)
                {
                    await _userProfileRepo.DeleteUserProfileAsync(item);
                }
            }
            string fileName = await Uploader.FileUploader(_webHost, new FileUploaderDto
            {
                UploadedFile = uploadUserProfileDto.UploadedFile,
                MediumType = MediumType.Image
            });

            UserProfile newUserProfile = new UserProfile()
            {
                Name = fileName,
                Path = $"uploads/{MediumType.Image}/{fileName}",
                UserId = userId,
                AppUser = appUser
            };
            await _userProfileRepo.AddUserProfileAsync(newUserProfile);
            return _mapper.Map<UserProfileDto>(newUserProfile);
        }

        public async Task DeleteUserProfileAsync(int userProfileId, string userId)
        {
            AppUser appUser = await _userManager.Users.Include(p => p.UserProfiles).FirstOrDefaultAsync(u => u.Id == userId) ?? throw new NotFoundException(UserErrorCodes.UserNotFound);
            UserProfile userProfile = await _userProfileRepo.GetUserProfileByIdAsync(userProfileId) ?? throw new NotFoundException(UserProfileErrorCodes.UserProfileNotFound);
            if (appUser.Id == userId || await _userManager.IsInRoleAsync(appUser, "Admin"))
            {
                await _userProfileRepo.DeleteUserProfileAsync(userProfile);
            }
        }

        public async Task<List<UserProfileDto>> GetAllProfilesAsync()
        {
            List<UserProfile> userProfiles = await _userProfileRepo.GetAllProfilesAsync();
            List<UserProfileDto> userProfileDtos = _mapper.Map<List<UserProfileDto>>(userProfiles);
            return userProfileDtos;
        }

        public async Task<UserProfileDto> GetUserProfileByIdAsync(int userProfileId)
        {
            UserProfile userProfile = await _userProfileRepo.GetUserProfileByIdAsync(userProfileId) ?? throw new NotFoundException(UserErrorCodes.UserNotFound);
            return _mapper.Map<UserProfileDto>(userProfile);
        }
    }
}