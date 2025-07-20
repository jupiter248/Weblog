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
        private readonly IPodcastService _podcastService;
        private readonly IArticleService _articleService;
        private readonly IEventService _eventService;
        public ViewContentService(
            IViewContentRepository viewContentRepo, IMapper mapper, UserManager<AppUser> userManager,
            IArticleService articleService, IPodcastService podcastService, IEventService eventService
            )
        {
            _viewContentRepo = viewContentRepo;
            _mapper = mapper;
            _userManager = userManager;
            _articleService = articleService;
            _eventService = eventService;
            _podcastService = podcastService;
        }
        public async Task AddViewContentAsync(string userId, int entityTypeId, LikeAndViewType entityType)
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
            ViewContent viewContent = new ViewContent
            {
                UserId = appUser.Id,
                AppUser = appUser,
                EntityId = entityTypeId,
                EntityType = entityType
            };
            await _viewContentRepo.AddViewContentAsync(viewContent);
        }

        public async Task<int> GetViewCountAsync(int entityTypeId, LikeAndViewType entityType)
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
            return await _viewContentRepo.GetViewCountAsync(entityTypeId, entityType);
        }
    }
}