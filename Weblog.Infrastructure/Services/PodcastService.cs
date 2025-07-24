using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Weblog.Application.CustomExceptions;
using Weblog.Application.Dtos.PodcastDtos;
using Weblog.Application.Interfaces.Repositories;
using Weblog.Application.Interfaces.Services;
using Weblog.Application.Queries;
using Weblog.Application.Queries.FilteringParams;
using Weblog.Domain.Enums;
using Weblog.Domain.Models;
using Weblog.Infrastructure.Extension;

namespace Weblog.Infrastructure.Services
{
    public class PodcastService : IPodcastService
    {
        private readonly IPodcastRepository _podcastRepo;
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepo;
        private readonly ITagRepository _tagRepo;
        private readonly IContributorRepository _contributorRepo;
        private readonly ILikeContentRepository _likeContentRepo;
        private readonly IViewContentRepository _viewContentRepo;
        public PodcastService(IViewContentRepository viewContentRepo, ILikeContentRepository likeContentRepo,   IPodcastRepository podcastRepo, IMapper mapper, ICategoryRepository categoryRepo, ITagRepository tagRepo, IContributorRepository contributorRepo)
        {
            _podcastRepo = podcastRepo;
            _mapper = mapper;
            _categoryRepo = categoryRepo;
            _tagRepo = tagRepo;
            _contributorRepo = contributorRepo;
            _likeContentRepo = likeContentRepo;
            _viewContentRepo = viewContentRepo;
        }
        public async Task AddContributorAsync(int podcastId, int contributorId)
        {
            Podcast podcast = await _podcastRepo.GetPodcastByIdAsync(podcastId) ?? throw new NotFoundException("Podcast not found");
            Contributor contributor = await _contributorRepo.GetContributorByIdAsync(contributorId) ?? throw new NotFoundException("Contributor not found");
            await _podcastRepo.AddContributorAsync(podcast , contributor);
        }

        public async Task<PodcastDto> AddPodcastAsync(AddPodcastDto addPodcastDto)
        {
            Podcast newPodcast = _mapper.Map<Podcast>(addPodcastDto);
            Category category = await _categoryRepo.GetCategoryByIdAsync(addPodcastDto.CategoryId) ?? throw new NotFoundException("Category not found");
            if (category.EntityType == CategoryType.Podcast)
            {
                newPodcast.Category = category;
            }
            newPodcast.Category = category;
            newPodcast.CreatedAt = DateTimeOffset.Now;
            newPodcast.Slug = newPodcast.Name.Slugify();

            if (newPodcast.IsDisplayed)
            {
                newPodcast.DisplayedAt = DateTimeOffset.Now;
            }
            Podcast addedPodcast =  await _podcastRepo.AddPodcastAsync(newPodcast);
            return _mapper.Map<PodcastDto>(addedPodcast);
        }

        public async Task AddTagAsync(int podcastId, int tagId)
        {
            Podcast podcast = await _podcastRepo.GetPodcastByIdAsync(podcastId) ?? throw new NotFoundException("Podcast not found");
            Tag tag = await _tagRepo.GetTagByIdAsync(tagId) ?? throw new NotFoundException("Tag not found");
            await _podcastRepo.AddTagAsync(podcast , tag);
        }

        public async Task DeleteContributorAsync(int podcastId, int contributorId)
        {
            Podcast podcast = await _podcastRepo.GetPodcastByIdAsync(podcastId) ?? throw new NotFoundException("Podcast not found");
            Contributor contributor = await _contributorRepo.GetContributorByIdAsync(contributorId) ?? throw new NotFoundException("Contributor not found");
            await _podcastRepo.DeleteContributorAsync(podcast , contributor);
        }

        public async Task DeletePodcastAsync(int podcastId)
        {
            Podcast podcast = await _podcastRepo.GetPodcastByIdAsync(podcastId) ?? throw new NotFoundException("Podcast not found");
            await _podcastRepo.DeletePodcastAsync(podcast);
        }

        public async Task DeleteTagAsync(int podcastId, int tagId)
        {
            Podcast podcast = await _podcastRepo.GetPodcastByIdAsync(podcastId) ?? throw new NotFoundException("Podcast not found");
            Tag tag = await _tagRepo.GetTagByIdAsync(tagId) ?? throw new NotFoundException("Tag not found");
            await _podcastRepo.DeleteTagAsync(podcast, tag);
        }

        public async Task<List<PodcastSummaryDto>> GetAllPodcastsAsync(PaginationParams paginationParams, FilteringParams filteringParams)
        {
            List<Podcast> podcasts = await _podcastRepo.GetAllPodcastsAsync(filteringParams, paginationParams);
            List<PodcastSummaryDto> podcastSummaryDtos = _mapper.Map<List<PodcastSummaryDto>>(podcasts);
            foreach (var item in podcastSummaryDtos)
            {
                item.LikeCount = await _likeContentRepo.GetLikeCountAsync(item.Id, LikeAndViewType.Podcast);
                item.ViewCount = await _viewContentRepo.GetViewCountAsync(item.Id, LikeAndViewType.Podcast);
            }
            return podcastSummaryDtos;
        }

        public async Task<PodcastDto> GetPodcastByIdAsync(int podcastId)
        {
            Podcast podcast = await _podcastRepo.GetPodcastByIdAsync(podcastId) ?? throw new NotFoundException("Podcast not found");
            PodcastDto podcastDto = _mapper.Map<PodcastDto>(podcast);
            podcastDto.LikeCount = await _likeContentRepo.GetLikeCountAsync(podcastDto.Id, LikeAndViewType.Podcast);
            podcastDto.ViewCount = await _viewContentRepo.GetViewCountAsync(podcastDto.Id, LikeAndViewType.Podcast);
            return podcastDto;
        }

        public async Task<List<PodcastSummaryDto>> GetSuggestionsAsync(PaginationParams paginationParams, int podcastId)
        {
            Podcast podcast = await _podcastRepo.GetPodcastByIdAsync(podcastId) ?? throw new NotFoundException("Podcast not found");
            List<Podcast> podcasts = await _podcastRepo.GetSuggestionsAsync(paginationParams, podcast);
            List<PodcastSummaryDto> podcastSummaryDtos = _mapper.Map<List<PodcastSummaryDto>>(podcasts);
            foreach (var item in podcastSummaryDtos)
            {
                item.LikeCount = await _likeContentRepo.GetLikeCountAsync(item.Id, LikeAndViewType.Event);
                item.ViewCount = await _viewContentRepo.GetViewCountAsync(item.Id, LikeAndViewType.Event);
            }
            return podcastSummaryDtos;  
       }

        public async Task UpdatePodcastAsync(UpdatePodcastDto updatePodcastDto, int podcastId)
        {
            Podcast podcast = await _podcastRepo.GetPodcastByIdAsync(podcastId) ?? throw new NotFoundException("Podcast not found");
            Category category = await _categoryRepo.GetCategoryByIdAsync(updatePodcastDto.CategoryId) ?? throw new NotFoundException("Category not found");

            podcast.Category = category;
            podcast.CategoryId = updatePodcastDto.CategoryId;
            podcast.Description = updatePodcastDto.Description;
            podcast.IsDisplayed = updatePodcastDto.IsDisplayed;
            podcast.Link = updatePodcastDto.Link;
            podcast.Name = updatePodcastDto.Name;
            podcast.Slug = podcast.Name.Slugify();
            if (podcast.IsDisplayed)
            {
                podcast.DisplayedAt = DateTimeOffset.Now;
            }
            podcast.UpdatedAt = DateTimeOffset.Now;
            await _podcastRepo.UpdatePodcastAsync(podcast);
        }
    }
}