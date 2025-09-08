using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Weblog.Application.CustomExceptions;
using Weblog.Application.Dtos.ArticleDtos;
using Weblog.Application.Dtos.CommentDtos;
using Weblog.Application.Dtos.EventDtos;
using Weblog.Application.Dtos.PodcastDtos;
using Weblog.Application.Interfaces.Repositories;
using Weblog.Application.Interfaces.Services;
using Weblog.Application.Queries;
using Weblog.Application.Queries.FilteringParams;
using Weblog.Domain.Enums;
using Weblog.Domain.Errors;
using Weblog.Domain.Errors.Comment;
using Weblog.Domain.Errors.Event;
using Weblog.Domain.Errors.Podcast;
using Weblog.Domain.Errors.User;
using Weblog.Domain.Models;

namespace Weblog.Infrastructure.Services
{
    public class CommentService : ICommentService
    {
        private readonly IMapper _mapper;   
        private readonly ICommentRepository _commentRepository;
        private readonly IArticleService _articleService; 
        private readonly IPodcastService _podcastService; 
        private readonly IEventService _eventService;
        private readonly UserManager<AppUser> _userManager;

        public CommentService(ICommentRepository commentRepository, UserManager<AppUser> userManager, IMapper mapper, IArticleService articleService, IPodcastService podcastService, IEventService eventService)
        {
            _mapper = mapper;
            _articleService = articleService;
            _eventService = eventService;
            _podcastService = podcastService;
            _userManager = userManager;
            _commentRepository = commentRepository;
        }

        public async Task<CommentDto> AddCommentAsync(AddCommentDto addCommentDto , string userId)
        {
            AppUser appUser = await _userManager.FindByIdAsync(userId) ?? throw new NotFoundException(UserErrorCodes.UserNotFound);
            switch (addCommentDto.EntityType)
            {
                case CommentType.Article:
                    ArticleDto articleDto = await _articleService.GetArticleByIdAsync(addCommentDto.EntityId) ?? throw new NotFoundException(ArticleErrorCodes.ArticleNotFound);
                    break;
                case CommentType.Event:
                    EventDto eventDto = await _eventService.GetEventByIdAsync(addCommentDto.EntityId) ?? throw new NotFoundException(EventErrorCodes.EventNotFound);
                    break;
                case CommentType.Podcast:
                    PodcastDto podcastDto = await _podcastService.GetPodcastByIdAsync(addCommentDto.EntityId) ?? throw new NotFoundException(PodcastErrorCodes.PodcastNotFound);
                    break;
                default:
                    throw new BadRequestException(CommentErrorCodes.CommentParentIdInvalid);
            }
            Comment comment = _mapper.Map<Comment>(addCommentDto);
            comment.UserId = appUser.Id;
            comment.AppUser = appUser;
            comment.TextedOn = DateTimeOffset.Now;
            Comment addedComment =  await _commentRepository.AddCommentAsync(comment);
            CommentDto commentDto = _mapper.Map<CommentDto>(addedComment);
            return commentDto;
        }

        public async Task DeleteCommentAsync(int commentId, string userId)
        {
            AppUser appUser = await _userManager.FindByIdAsync(userId) ?? throw new NotFoundException(UserErrorCodes.UserNotFound);
            Comment comment = await _commentRepository.GetCommentByIdAsync(commentId) ?? throw new NotFoundException(CommentErrorCodes.CommentNotFound);
            var userRoles = await _userManager.GetRolesAsync(appUser); 
            if (comment.UserId != appUser.Id || !userRoles.Contains("Admin"))
            {
                throw new ForbiddenException(CommentErrorCodes.CommentDeleteForbidden , []);
            }
            await _commentRepository.DeleteCommentAsync(comment);
        }

        public async Task<List<CommentDto>> GetAllCommentsAsync(CommentFilteringParams commentFilteringParams, PaginationParams paginationParams)
        {
            List<Comment> comments = await _commentRepository.GetAllCommentsAsync(commentFilteringParams, paginationParams);
            List<CommentDto> commentDtos = _mapper.Map<List<CommentDto>>(comments);
            return commentDtos;
        }

        public async Task<CommentDto> GetCommentByIdAsync(int commentId)
        {
            Comment comment = await _commentRepository.GetCommentByIdAsync(commentId) ?? throw new NotFoundException(CommentErrorCodes.CommentNotFound);
            return _mapper.Map<CommentDto>(comment);
        }

        public async Task<CommentDto> UpdateCommentAsync(UpdateCommentDto updateCommentDto, int commentId, string userId)
        {
            AppUser appUser = await _userManager.FindByIdAsync(userId) ?? throw new NotFoundException(UserErrorCodes.UserNotFound);
            Comment comment = await _commentRepository.GetCommentByIdAsync(commentId) ?? throw new NotFoundException(CommentErrorCodes.CommentNotFound);
            var userRoles = await _userManager.GetRolesAsync(appUser);
            if (comment.UserId != appUser.Id || !userRoles.Contains("Admin"))
            {
                throw new ForbiddenException(CommentErrorCodes.CommentUpdateForbidden, []);
            }
            comment = _mapper.Map(updateCommentDto, comment);
            await _commentRepository.UpdateCommentAsync(comment);
            return _mapper.Map<CommentDto>(comment);
        }
    }
}