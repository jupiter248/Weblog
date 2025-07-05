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
using Weblog.Domain.Models;
using Weblog.Infrastructure.Identity;

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
        private readonly UserManager<AppUser> _userManager;
        public MediumService(
            IMapper mapper, IMediumRepository mediumRepo, IWebHostEnvironment webHost,
            IArticleService articleService, IPodcastService podcastService, IEventService eventService,
            IContributorService contributorService, UserManager<AppUser> userManager
            )
        {
            _mapper = mapper;
            _webHost = webHost;
            _mediumRepo = mediumRepo;
            _articleService = articleService;
            _contributorService = contributorService;
            _eventService = eventService;
            _podcastService = podcastService;
            _userManager = userManager;
        }

        public async Task DeleteMediumAsync(int mediaId)
        {
            Medium medium = await _mediumRepo.GetMediumByIdAsync(mediaId) ?? throw new NotFoundException("Media not found");

            string filePath = Path.Combine(_webHost.WebRootPath, medium.Path);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            await _mediumRepo.DeleteMediumAsync(medium);
        }

        public async Task<List<MediumDto>> GetAllMediaAsync()
        {
            List<Medium> media = await _mediumRepo.GetAllMediaAsync();
            List<MediumDto> mediaDtos = _mapper.Map<List<MediumDto>>(media);
            return mediaDtos;
        }

        public async Task<MediumDto> GetMediumByIdAsync(int mediaId)
        {
            Medium medium = await _mediumRepo.GetMediumByIdAsync(mediaId) ?? throw new NotFoundException("Media not found");
            return _mapper.Map<MediumDto>(medium);
        }

        public async Task<MediumDto> StoreMediumAsync(UploadMediumDto uploadMediaDto)
        {
            switch (uploadMediaDto.EntityType)
            {
                case EntityType.Article:
                    ArticleDto articleDto = await _articleService.GetArticleByIdAsync(uploadMediaDto.ParentTypeId) ?? throw new NotFoundException("Article not found");
                    break;
                case EntityType.Event:
                    EventDto eventDto = await _eventService.GetEventByIdAsync(uploadMediaDto.ParentTypeId) ?? throw new NotFoundException("Event not found");
                    break;
                case EntityType.Contributor:
                    ContributorDto contributorDto = await _contributorService.GetContributorByIdAsync(uploadMediaDto.ParentTypeId) ?? throw new NotFoundException("Contributor not found");
                    break;
                case EntityType.Podcast:
                    PodcastDto podcastDto = await _podcastService.GetPodcastByIdAsync(uploadMediaDto.ParentTypeId) ?? throw new NotFoundException("Podcast not found");
                    break;
                case EntityType.User:
                    var appUser = await _userManager.FindByIdAsync(uploadMediaDto.AppUserId ?? throw new NotFoundException("Username not found"));
                    break;

                default:
                    throw new ValidationException("The Id is invalid");
            }


            IFormFile mediumFile = uploadMediaDto.UploadedFile;
            if (mediumFile == null || mediumFile.Length == 0)
            {
                throw new ArgumentException("Invalid file");
            }

            var uploadsFolder = Path.Combine(_webHost.WebRootPath, "uploads");

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }
            if (!Directory.Exists(Path.Combine(_webHost.WebRootPath, $"uploads/{uploadMediaDto.MediumType}")))
            {
                Directory.CreateDirectory(Path.Combine(_webHost.WebRootPath, $"uploads/{uploadMediaDto.MediumType}"));
            }

            var fileName = $"{Guid.NewGuid()}-{Path.GetFileName(mediumFile.FileName)}";
            var filePath = Path.Combine(_webHost.WebRootPath, $"uploads/{uploadMediaDto.MediumType}", fileName);
            

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await mediumFile.CopyToAsync(stream);
            }
            
            Medium medium = new Medium
            {
                Name = fileName,
                Path = $"uploads/{uploadMediaDto.MediumType}/{fileName}",
                IsPrimary = uploadMediaDto.IsPrimary,
                MediumType = uploadMediaDto.MediumType,
                EntityType = uploadMediaDto.EntityType,
                EntityId = uploadMediaDto.ParentTypeId,
                Username = uploadMediaDto.AppUserId
            };

            await _mediumRepo.AddMediumAsync(medium);
            return _mapper.Map<MediumDto>(medium);
        }

        public async Task UpdateMediumAsync(UpdateMediumDto updateMediumDto, int mediaId)
        {
            Medium medium = await _mediumRepo.GetMediumByIdAsync(mediaId) ?? throw new NotFoundException("Media not found");
            medium.Name = updateMediumDto.Name;
            medium.Path = updateMediumDto.Path;
            medium.IsPrimary = updateMediumDto.IsPrimary;
            await _mediumRepo.UpdateMediumAsync(medium);
        }
    }
}