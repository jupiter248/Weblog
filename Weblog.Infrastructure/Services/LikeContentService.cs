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
    public class LikeContentService : ILikeContentService
    {
        private readonly ILikeContentRepository _likeContentRepo;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly IPodcastService _podcastService;
        private readonly IArticleService _articleService;
        private readonly IEventService _eventService;

        public LikeContentService(
            ILikeContentRepository likeContentRepo, IMapper mapper, UserManager<AppUser> userManager,
            IArticleService articleService, IPodcastService podcastService, IEventService eventService
            )
        {
            _likeContentRepo = likeContentRepo;
            _mapper = mapper;
            _userManager = userManager;
            _articleService = articleService;
            _eventService = eventService;
            _podcastService = podcastService;
        }
        public async Task AddLikeContentAsync(string userId, int entityTypeId, LikeAndViewType entityType)
        {
            AppUser? appUser = await _userManager.FindByIdAsync(userId) ?? throw new NotFoundException("User not found");
            switch (entityType)
            {
                case LikeAndViewType.Article:
                    ArticleDto articleDto = await _articleService.GetArticleByIdAsync(entityTypeId) ?? throw new NotFoundException("Article not found");
                    break;
                case LikeAndViewType.Event:
                    EventDto eventDto = await _eventService.GetEventByIdAsync(entityTypeId) ?? throw new NotFoundException("Event not found");
                    break;
                case LikeAndViewType.Podcast:
                    PodcastDto podcastDto = await _podcastService.GetPodcastByIdAsync(entityTypeId) ?? throw new NotFoundException("Podcast not found");
                    break;

                default:
                    throw new ValidationException("The Id is invalid");
            }
            LikeContent likeContent = new LikeContent
            {
                UserId = appUser.Id,
                AppUser = appUser,
                EntityId = entityTypeId,
                EntityType = entityType
            };
            await _likeContentRepo.AddLikeContentAsync(likeContent);
        }

        public async Task DeleteLikeContentAsync(string userId, int entityTypeId, LikeAndViewType entityType)
        {
            AppUser? appUser = await _userManager.FindByIdAsync(userId) ?? throw new NotFoundException("User not found");
            switch (entityType)
            {
                case LikeAndViewType.Article:
                    ArticleDto articleDto = await _articleService.GetArticleByIdAsync(entityTypeId) ?? throw new NotFoundException("Article not found");
                    break;
                case LikeAndViewType.Event:
                    EventDto eventDto = await _eventService.GetEventByIdAsync(entityTypeId) ?? throw new NotFoundException("Event not found");
                    break;
                case LikeAndViewType.Podcast:
                    PodcastDto podcastDto = await _podcastService.GetPodcastByIdAsync(entityTypeId) ?? throw new NotFoundException("Podcast not found");
                    break;

                default:
                    throw new ValidationException("The Id is invalid");
            }
            LikeContent likeContent = new LikeContent
            {
                UserId = appUser.Id,
                AppUser = appUser,
                EntityId = entityTypeId,
                EntityType = entityType
            };
            await _likeContentRepo.DeleteLikeContentAsync(likeContent); 
        }

        public async Task<List<UserDto>> GetAllContentLikesAsync(int entityTypeId, LikeAndViewType entityType)
        {
            List<LikeContent> likeContents = await _likeContentRepo.GetAllContentLikesAsync(entityTypeId, entityType);
            List<UserDto> userDtos = _mapper.Map<List<UserDto>>(likeContents.Select(l => l.AppUser));
            return userDtos;
        }

        public async Task<int> GetLikeCountAsync(int entityTypeId, LikeAndViewType entityType)
        {
            switch (entityType)
            {
                case LikeAndViewType.Article:
                    ArticleDto articleDto = await _articleService.GetArticleByIdAsync(entityTypeId) ?? throw new NotFoundException("Article not found");
                    break;
                case LikeAndViewType.Event:
                    EventDto eventDto = await _eventService.GetEventByIdAsync(entityTypeId) ?? throw new NotFoundException("Event not found");
                    break;
                case LikeAndViewType.Podcast:
                    PodcastDto podcastDto = await _podcastService.GetPodcastByIdAsync(entityTypeId) ?? throw new NotFoundException("Podcast not found");
                    break;

                default:
                    throw new ValidationException("The Id is invalid");
            }
            return await _likeContentRepo.GetLikeCountAsync(entityTypeId , entityType);
        }
    }
}