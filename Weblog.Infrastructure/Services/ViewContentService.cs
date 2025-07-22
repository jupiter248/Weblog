using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Weblog.Application.CustomExceptions;
using Weblog.Application.Dtos.ArticleDtos;
using Weblog.Application.Dtos.EventDtos;
using Weblog.Application.Dtos.PodcastDtos;
using Weblog.Application.Dtos.UserDtos;
using Weblog.Application.Interfaces.Repositories;
using Weblog.Application.Interfaces.Services;
using Weblog.Domain.Enums;
using Weblog.Domain.JoinModels;
using Weblog.Domain.Models;

namespace Weblog.Infrastructure.Services
{
    public class ViewContentService : IViewContentService
    {
        private readonly IViewContentRepository _viewContentRepo;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly IContentExistenceService _contentExistenceService;
        public ViewContentService(
            IViewContentRepository viewContentRepo, IMapper mapper, UserManager<AppUser> userManager,
            IContentExistenceService contentExistenceService
            )
        {
            _viewContentRepo = viewContentRepo;
            _mapper = mapper;
            _userManager = userManager;
            _contentExistenceService = contentExistenceService;
        }
        public async Task AddViewContentAsync(string userId, int entityTypeId, LikeAndViewType entityType)
        {
            AppUser? appUser = await _userManager.FindByIdAsync(userId) ?? throw new NotFoundException("User not found");
            if (!await _contentExistenceService.ContentExistsAsync(entityTypeId , entityType))
            {
                throw new NotFoundException("Content not found");   
            }
            if (await _viewContentRepo.IsViewedAsync(userId , entityTypeId , entityType))
            {
                throw new ValidationException("Yoy already viewed");
            }
            await _viewContentRepo.ViewAsync(appUser ,entityTypeId , entityType);
        }

        public async Task<List<UserDto>> GetAllContentViewersAsync(int entityTypeId, LikeAndViewType entityType)
        {
            List<ViewContent> viewContents = await _viewContentRepo.GetAllContentViewersAsync(entityTypeId, entityType);
            List<UserDto> userDtos = _mapper.Map<List<UserDto>>(viewContents.Select(l => l.AppUser));
            return userDtos;
        }

        public async Task<int> GetViewCountAsync(int entityTypeId, LikeAndViewType entityType)
        {
            if (!await _contentExistenceService.ContentExistsAsync(entityTypeId , entityType))
            {
                throw new NotFoundException("Content not found");   
            }   
            return await _viewContentRepo.GetViewCountAsync(entityTypeId, entityType);
        }

        public async Task<bool> IsViewedAsync(string userId, int entityTypeId, LikeAndViewType entityType)
        {
            if (!await _contentExistenceService.ContentExistsAsync(entityTypeId, entityType))
            {
                throw new NotFoundException("Content not found");
            }
            return await _viewContentRepo.IsViewedAsync(userId ,entityTypeId , entityType);
        }
    }
}