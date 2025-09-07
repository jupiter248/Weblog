using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Weblog.Application.CustomExceptions;
using Weblog.Application.Dtos.ArticleDtos;
using Weblog.Application.Dtos.EventDtos;
using Weblog.Application.Dtos.LikeContentDtos;
using Weblog.Application.Dtos.PodcastDtos;
using Weblog.Application.Dtos.UserDtos;
using Weblog.Application.Interfaces.Repositories;
using Weblog.Application.Interfaces.Services;
using Weblog.Domain.Enums;
using Weblog.Domain.Errors.Common;
using Weblog.Domain.Errors.LikeContent;
using Weblog.Domain.Errors.User;
using Weblog.Domain.JoinModels;
using Weblog.Domain.Models;

namespace Weblog.Infrastructure.Services
{
    public class LikeContentService : ILikeContentService
    {
        private readonly ILikeContentRepository _likeContentRepo;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly IContentExistenceService _contentExistenceService;

        public LikeContentService(
            ILikeContentRepository likeContentRepo, IMapper mapper, UserManager<AppUser> userManager,
            IContentExistenceService contentExistenceService
            )
        {
            _likeContentRepo = likeContentRepo;
            _mapper = mapper;
            _userManager = userManager;
            _contentExistenceService = contentExistenceService;

        }
        public async Task<List<UserDto>> GetAllContentLikesAsync(int entityTypeId, LikeAndViewType entityType)
        {
            List<LikeContent> likeContents = await _likeContentRepo.GetAllContentLikesAsync(entityTypeId, entityType);
            List<UserDto> userDtos = _mapper.Map<List<UserDto>>(likeContents.Select(l => l.AppUser));
            return userDtos;
        }
        public async Task<int> GetLikeCountAsync(int entityTypeId, LikeAndViewType entityType)
        {
            return await _likeContentRepo.GetLikeCountAsync(entityTypeId , entityType);
        }

        public async Task<bool> IsLikedAsync(string userId, int entityTypeId, LikeAndViewType entityType)
        {
          return await _likeContentRepo.IsLikedAsync(userId, entityTypeId, entityType);
        }

        public async Task LikeAsync(string userId, LikeContentDto likeContentDto)
        {
            if (!await _contentExistenceService.ContentExistsAsync(likeContentDto.EntityTypeId, likeContentDto.EntityType))
            {
                throw new NotFoundException(CommonErrorCodes.ContentNotFound);
            }
            if (await _likeContentRepo.IsLikedAsync(userId, likeContentDto.EntityTypeId, likeContentDto.EntityType))
            {
                throw new ConflictException(LikeContentErrorCodes.AlreadyLiked);
            }
            AppUser appUser = await _userManager.FindByIdAsync(userId) ?? throw new NotFoundException(UserErrorCodes.UserNotFound);
            await _likeContentRepo.LikeAsync(appUser , likeContentDto);
        }

        public async Task UnlikeAsync(string userId, UnLikeContentDto unLikeContentDto)
        {
            if (!await _contentExistenceService.ContentExistsAsync(unLikeContentDto.EntityTypeId, unLikeContentDto.EntityType))
            {
                throw new NotFoundException(CommonErrorCodes.ContentNotFound);
            }
            if (!await _likeContentRepo.IsLikedAsync(userId, unLikeContentDto.EntityTypeId, unLikeContentDto.EntityType))
            {
                throw new ConflictException(LikeContentErrorCodes.AlreadyLiked);
            }
            await _likeContentRepo.UnlikeAsync(userId, unLikeContentDto);
        }
    }
}