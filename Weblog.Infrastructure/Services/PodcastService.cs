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
using Weblog.Domain.Errors.Category;
using Weblog.Domain.Errors.Contributor;
using Weblog.Domain.Errors.Podcast;
using Weblog.Domain.Errors.Tag;
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
        public PodcastService( ILikeContentRepository likeContentRepo,   IPodcastRepository podcastRepo, IMapper mapper, ICategoryRepository categoryRepo, ITagRepository tagRepo, IContributorRepository contributorRepo)
        {
            _podcastRepo = podcastRepo;
            _mapper = mapper;
            _categoryRepo = categoryRepo;
            _tagRepo = tagRepo;
            _contributorRepo = contributorRepo;
            _likeContentRepo = likeContentRepo;
        }
        public async Task AddContributorAsync(int podcastId, int contributorId)
        {
            Podcast podcast = await _podcastRepo.GetPodcastByIdAsync(podcastId) ?? throw new NotFoundException(PodcastErrorCodes.PodcastNotFound);
            Contributor contributor = await _contributorRepo.GetContributorByIdAsync(contributorId) ?? throw new NotFoundException(ContributorErrorCodes.ContributorNotFound);
            await _podcastRepo.AddContributorAsync(podcast , contributor);
        }

        public async Task<PodcastDto> AddPodcastAsync(AddPodcastDto addPodcastDto)
        {
            Podcast newPodcast = _mapper.Map<Podcast>(addPodcastDto);
            Category category = await _categoryRepo.GetCategoryByIdAsync(addPodcastDto.CategoryId) ?? throw new NotFoundException(CategoryErrorCodes.CategoryNotFound);
            if (category.EntityType == CategoryType.Podcast)
            {
                newPodcast.Category = category;
            }
            else
            {
                throw new ConflictException(CategoryErrorCodes.CategoryEntityTypeMatchFailed);
            }   
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
            Podcast podcast = await _podcastRepo.GetPodcastByIdAsync(podcastId) ?? throw new NotFoundException(PodcastErrorCodes.PodcastNotFound);
            Tag tag = await _tagRepo.GetTagByIdAsync(tagId) ?? throw new NotFoundException(TagErrorCodes.TagNotFound);
            await _podcastRepo.AddTagAsync(podcast , tag);
        }

        public async Task DeleteContributorAsync(int podcastId, int contributorId)
        {
            Podcast podcast = await _podcastRepo.GetPodcastByIdAsync(podcastId) ?? throw new NotFoundException(PodcastErrorCodes.PodcastNotFound);
            Contributor contributor = await _contributorRepo.GetContributorByIdAsync(contributorId) ?? throw new NotFoundException(ContributorErrorCodes.ContributorNotFound);
            await _podcastRepo.DeleteContributorAsync(podcast , contributor);
        }

        public async Task DeletePodcastAsync(int podcastId)
        {
            Podcast podcast = await _podcastRepo.GetPodcastByIdAsync(podcastId) ?? throw new NotFoundException(PodcastErrorCodes.PodcastNotFound);
            await _podcastRepo.DeletePodcastAsync(podcast);
        }

        public async Task DeleteTagAsync(int podcastId, int tagId)
        {
            Podcast podcast = await _podcastRepo.GetPodcastByIdAsync(podcastId) ?? throw new NotFoundException(PodcastErrorCodes.PodcastNotFound);
            Tag tag = await _tagRepo.GetTagByIdAsync(tagId) ?? throw new NotFoundException(TagErrorCodes.TagNotFound);
            await _podcastRepo.DeleteTagAsync(podcast, tag);
        }

        public async Task<List<PodcastSummaryDto>> GetAllPodcastsAsync(PaginationParams paginationParams, PodcastFilteringParams podcastFilteringParams)
        {
            List<Podcast> podcasts = await _podcastRepo.GetAllPodcastsAsync(podcastFilteringParams, paginationParams);
            List<PodcastSummaryDto> podcastSummaryDtos = _mapper.Map<List<PodcastSummaryDto>>(podcasts);
            foreach (var item in podcastSummaryDtos)
            {
                item.LikeCount = await _likeContentRepo.GetLikeCountAsync(item.Id, LikeAndViewType.Podcast);
            }
            if (podcastFilteringParams.MostLikes == true)
            {
                podcastSummaryDtos = podcastSummaryDtos.OrderByDescending(l => l.LikeCount).ToList();
            }
            else if (podcastFilteringParams.MostLikes == false)
            {
                podcastSummaryDtos = podcastSummaryDtos.OrderBy(l => l.LikeCount).ToList();
            }

            if (podcastFilteringParams.MostViews == true)
            {
                podcastSummaryDtos = podcastSummaryDtos.OrderByDescending(l => l.ViewCount).ToList();
            }
            else if (podcastFilteringParams.MostViews == false)
            {
                podcastSummaryDtos = podcastSummaryDtos.OrderBy(l => l.ViewCount).ToList();
            }
            return podcastSummaryDtos;
        }

        public async Task<PodcastDto> GetPodcastByIdAsync(int podcastId)
        {
            Podcast podcast = await _podcastRepo.GetPodcastByIdAsync(podcastId) ?? throw new NotFoundException(PodcastErrorCodes.PodcastNotFound);
            PodcastDto podcastDto = _mapper.Map<PodcastDto>(podcast);
            podcastDto.LikeCount = await _likeContentRepo.GetLikeCountAsync(podcastDto.Id, LikeAndViewType.Podcast);
            return podcastDto;
        }

        public async Task<List<PodcastSummaryDto>> GetSuggestionsAsync(PaginationParams paginationParams, int podcastId)
        {
            Podcast podcast = await _podcastRepo.GetPodcastByIdAsync(podcastId) ?? throw new NotFoundException(PodcastErrorCodes.PodcastNotFound);
            List<Podcast> podcasts = await _podcastRepo.GetSuggestionsAsync(paginationParams, podcast);
            List<PodcastSummaryDto> podcastSummaryDtos = _mapper.Map<List<PodcastSummaryDto>>(podcasts);
            foreach (var item in podcastSummaryDtos)
            {
                item.LikeCount = await _likeContentRepo.GetLikeCountAsync(item.Id, LikeAndViewType.Event);
            }
            return podcastSummaryDtos;  
       }

        public async Task<int> IncrementPodcastViewAsync(int podcastId)
        {
            Podcast podcast = await _podcastRepo.GetPodcastByIdAsync(podcastId) ?? throw new NotFoundException(PodcastErrorCodes.PodcastNotFound);
            await _podcastRepo.IncrementEventViewAsync(podcast);
            return podcast.ViewCount;
        }

        public async Task UpdatePodcastAsync(UpdatePodcastDto updatePodcastDto, int podcastId)
        {
            Podcast podcast = await _podcastRepo.GetPodcastByIdAsync(podcastId) ?? throw new NotFoundException(PodcastErrorCodes.PodcastNotFound);
            Category category = await _categoryRepo.GetCategoryByIdAsync(updatePodcastDto.CategoryId) ?? throw new NotFoundException(CategoryErrorCodes.CategoryNotFound);
            if (category.EntityType == CategoryType.Podcast)
            {
                podcast.Category = category;
            }
            else
            {
                throw new ConflictException(CategoryErrorCodes.CategoryEntityTypeMatchFailed);
            }
            podcast = _mapper.Map(updatePodcastDto, podcast);
            podcast.UpdatedAt = DateTimeOffset.Now;
            podcast.Slug = podcast.Name.Slugify();
            if (podcast.IsDisplayed == true)
            {
                if (podcast.DisplayedAt == DateTimeOffset.MinValue)
                {
                    podcast.DisplayedAt = DateTimeOffset.Now;
                }
            }
            else
            {
                if (podcast.DisplayedAt != DateTimeOffset.MinValue)
                {
                    podcast.DisplayedAt = DateTimeOffset.MinValue;
                }
            }
            await _podcastRepo.UpdatePodcastAsync(podcast);
        }
    }
}