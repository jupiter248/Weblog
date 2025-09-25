using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Weblog.Application.CustomExceptions;
using Weblog.Application.Dtos.ArticleDtos;
using Weblog.Application.Dtos.ContributorDtos;
using Weblog.Application.Dtos.EventDtos;
using Weblog.Application.Dtos.MediaDtos;
using Weblog.Application.Dtos.PodcastDtos;
using Weblog.Application.Interfaces.Repositories;
using Weblog.Application.Interfaces.Services;
using Weblog.Domain.Enums;
using Weblog.Domain.Errors;
using Weblog.Domain.Errors.Common;
using Weblog.Domain.Errors.Contributor;
using Weblog.Domain.Errors.Event;
using Weblog.Domain.Errors.Medium;
using Weblog.Domain.Errors.Podcast;
using Weblog.Domain.Errors.User;
using Weblog.Domain.Models;
using Weblog.Infrastructure.Helpers;

namespace Weblog.Infrastructure.Services
{
    public class MediumService : IMediumService
    {
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHost;
        private readonly IMediumRepository _mediumRepo; 
        private readonly IArticleService _articleService; 
        private readonly IPodcastService _podcastService; 
        private readonly IEventService _eventService; 
        private readonly IContributorService _contributorService;
        public MediumService(
            IMapper mapper, IMediumRepository mediumRepo, IWebHostEnvironment webHost,
            IArticleService articleService, IPodcastService podcastService, IEventService eventService,
            IContributorService contributorService
            )
        {
            _mapper = mapper;
            _webHost = webHost;
            _mediumRepo = mediumRepo;
            _articleService = articleService;
            _contributorService = contributorService;
            _eventService = eventService;
            _podcastService = podcastService;
        }

        public async Task DeleteMediumAsync(int mediaId)
        {
            Medium medium = await _mediumRepo.GetMediumByIdAsync(mediaId) ?? throw new NotFoundException(MediumErrorCodes.MediumNotFound);
            await _mediumRepo.DeleteMediumAsync(medium);
            await FileManager.DeleteFile(_webHost, medium.Path);
        }

        public async Task<List<MediumDto>> GetAllMediaAsync()
        {
            List<Medium> media = await _mediumRepo.GetAllMediaAsync();
            List<MediumDto> mediaDtos = _mapper.Map<List<MediumDto>>(media);
            return mediaDtos;
        }

        public async Task<MediumDto> GetMediumByIdAsync(int mediaId)
        {
            Medium medium = await _mediumRepo.GetMediumByIdAsync(mediaId) ?? throw new NotFoundException(MediumErrorCodes.MediumNotFound);
            return _mapper.Map<MediumDto>(medium);
        }

        public async Task<MediumDto> StoreMediumAsync(UploadMediumDto uploadMediaDto)
        {
            switch (uploadMediaDto.EntityType)
            {
                case EntityType.Article:
                    ArticleDto articleDto = await _articleService.GetArticleByIdAsync(uploadMediaDto.EntityId) ?? throw new NotFoundException(ArticleErrorCodes.ArticleNotFound);
                    break;
                case EntityType.Event:
                    EventDto eventDto = await _eventService.GetEventByIdAsync(uploadMediaDto.EntityId) ?? throw new NotFoundException(EventErrorCodes.EventNotFound);
                    break;
                case EntityType.Contributor:
                    ContributorDto contributorDto = await _contributorService.GetContributorByIdAsync(uploadMediaDto.EntityId) ?? throw new NotFoundException(ContributorErrorCodes.ContributorNotFound);
                    break;
                case EntityType.Podcast:
                    PodcastDto podcastDto = await _podcastService.GetPodcastByIdAsync(uploadMediaDto.EntityId) ?? throw new NotFoundException(PodcastErrorCodes.PodcastNotFound);
                    break;
                default:
                    throw new BadRequestException(MediumErrorCodes.MediumParentIdInvalid);
            }

            string fileName = await FileManager.UploadFile(_webHost , new FileUploaderDto
            {
                UploadedFile = uploadMediaDto.UploadedFile,
                MediumType = uploadMediaDto.MediumType
            });

            Medium medium = new Medium
            {
                Name = fileName,
                Path = $"uploads/{uploadMediaDto.MediumType}/{fileName}",
                IsPrimary = uploadMediaDto.IsPrimary,
                IsOnPoster = uploadMediaDto.IsOnPoster,
                MediumType = uploadMediaDto.MediumType,
                EntityType = uploadMediaDto.EntityType,
                EntityId = uploadMediaDto.EntityId,
                AltText = uploadMediaDto.AltText
            };

            await _mediumRepo.AddMediumAsync(medium);
            return _mapper.Map<MediumDto>(medium);
        }

        public async Task<MediumDto> UpdateMediumAsync(UpdateMediumDto updateMediumDto, int mediaId)
        {
            Medium medium = await _mediumRepo.GetMediumByIdAsync(mediaId) ?? throw new NotFoundException(MediumErrorCodes.MediumNotFound);
            medium = _mapper.Map(updateMediumDto, medium);
            await _mediumRepo.UpdateMediumAsync(medium);
            return _mapper.Map<MediumDto>(medium);
        }
    }
}