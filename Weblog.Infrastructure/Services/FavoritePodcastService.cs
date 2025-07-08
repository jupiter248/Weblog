using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Weblog.Application.CustomExceptions;
using Weblog.Application.Dtos.PodcastDtos;
using Weblog.Application.Interfaces.Repositories;
using Weblog.Application.Interfaces.Services;
using Weblog.Application.Queries;
using Weblog.Application.Queries.FilteringParams;
using Weblog.Domain.JoinModels;
using Weblog.Domain.Models;

namespace Weblog.Infrastructure.Services
{
    public class FavoritePodcastService : IFavoritePodcastService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IFavoritePodcastRepository _favoritePodcastRepo;
        private readonly IPodcastRepository _podcastRepo;
        private readonly IMapper _mapper;

        public FavoritePodcastService(IPodcastRepository podcastRepo, IMapper mapper, UserManager<AppUser> userManager, IFavoritePodcastRepository favoritePodcastRepo)
        {
            _userManager = userManager;
            _favoritePodcastRepo = favoritePodcastRepo;
            _mapper = mapper;
            _podcastRepo = podcastRepo;
        }

        public async Task AddPodcastToFavoriteAsync(int podcastId, string userId)
        {
            AppUser appUser = await _userManager.FindByIdAsync(userId) ?? throw new NotFoundException("User not found");
            Podcast podcast = await _podcastRepo.GetPodcastByIdAsync(podcastId) ?? throw new NotFoundException("Podcast not found");
            bool podcastAdded = await _favoritePodcastRepo.PodcastAddedToFavoriteAsync(new FavoritePodcast { PodcastId = podcastId, UserId = userId });
            if(podcastAdded == true)
            {
                throw new ValidationException("The podcast already added into favorites");
            }
            FavoritePodcast favoritePodcast = new FavoritePodcast
            {
                UserId = appUser.Id,
                AppUser = appUser,
                PodcastId = podcastId,
                Podcast = podcast
            };
            await _favoritePodcastRepo.AddPodcastToFavoriteAsync(favoritePodcast);
        }

        public async Task DeletePodcastFromFavoriteAsync(int podcastId, string userId)
        {
            AppUser appUser = await _userManager.FindByIdAsync(userId) ?? throw new NotFoundException("User not found");
            FavoritePodcast favoritePodcast = await _favoritePodcastRepo.GetFavoritePodcastByIdAsync(podcastId) ?? throw new NotFoundException("Favorite podcast not found");
            if (appUser.Id != favoritePodcast.UserId)
            {
                throw new ValidationException("Favorite event not found");
            }

            await _favoritePodcastRepo.DeletePodcastFromFavoriteAsync(favoritePodcast);        }

        public async Task<List<PodcastDto>> GetAllFavoritePodcastsAsync(string userId)
        {
            AppUser appUser = await _userManager.FindByIdAsync(userId) ?? throw new NotFoundException("User not found");
            List<FavoritePodcast> favoritePodcasts = await _favoritePodcastRepo.GetAllFavoritePodcastsAsync(userId);
            List<PodcastDto> podcastDtos = _mapper.Map<List<PodcastDto>>(favoritePodcasts.Select(f => f.Podcast));
            return podcastDtos;
        }
    }
}