using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Weblog.Application.CustomExceptions;
using Weblog.Application.Dtos.FavoritesDtos.EventFavoriteDtos;
using Weblog.Application.Dtos.FavoritesDtos.PodcastFavoriteDto;
using Weblog.Application.Dtos.PodcastDtos;
using Weblog.Application.Interfaces.Repositories;
using Weblog.Application.Interfaces.Services;
using Weblog.Application.Queries;
using Weblog.Application.Queries.FilteringParams;
using Weblog.Domain.Errors.Favorite;
using Weblog.Domain.Errors.Podcast;
using Weblog.Domain.Errors.User;
using Weblog.Domain.JoinModels;
using Weblog.Domain.JoinModels.Favorites;
using Weblog.Domain.Models;

namespace Weblog.Infrastructure.Services
{
    public class FavoritePodcastService : IFavoritePodcastService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IFavoritePodcastRepository _favoritePodcastRepo;
        private readonly IPodcastRepository _podcastRepo;
        private readonly IFavoriteListRepository _favoriteListRepo;
        private readonly IMapper _mapper;

        public FavoritePodcastService(IFavoriteListRepository favoriteListRepo, IPodcastRepository podcastRepo, IMapper mapper, UserManager<AppUser> userManager, IFavoritePodcastRepository favoritePodcastRepo)
        {
            _userManager = userManager;
            _favoritePodcastRepo = favoritePodcastRepo;
            _mapper = mapper;
            _podcastRepo = podcastRepo;
            _favoriteListRepo = favoriteListRepo;
        }

        public async Task AddPodcastToFavoriteAsync(string userId,AddFavoritePodcastDto addFavoritePodcastDto) 
        {
            AppUser appUser = await _userManager.FindByIdAsync(userId) ?? throw new NotFoundException(UserErrorCodes.UserNotFound);
            Podcast podcast = await _podcastRepo.GetPodcastByIdAsync(addFavoritePodcastDto.PodcastId) ?? throw new NotFoundException(PodcastErrorCodes.PodcastNotFound);
            if (addFavoritePodcastDto.favoriteListId.HasValue)
            {
                FavoriteList favoriteList = await _favoriteListRepo.GetFavoriteListByIdAsync(addFavoritePodcastDto.favoriteListId) ?? throw new NotFoundException(FavoriteErrorCodes.FavoriteListNotFound);
            }

            bool podcastAdded = await _favoritePodcastRepo.PodcastAddedToFavoriteAsync(new FavoritePodcast { PodcastId = addFavoritePodcastDto.PodcastId, UserId = userId });
            if(podcastAdded == true)
            {
                throw new ConflictException(FavoriteErrorCodes.FavoriteAlreadyExists);
            }
            FavoritePodcast favoritePodcast = new FavoritePodcast
            {
                UserId = appUser.Id,
                AppUser = appUser,
                PodcastId = podcast.Id,
                Podcast = podcast,
                FavoriteListId = addFavoritePodcastDto.favoriteListId
            };
            await _favoritePodcastRepo.AddPodcastToFavoriteAsync(favoritePodcast);
        }

        public async Task DeletePodcastFromFavoriteAsync(int podcastId, string userId)
        {
            AppUser appUser = await _userManager.FindByIdAsync(userId) ?? throw new NotFoundException(UserErrorCodes.UserNotFound);
            FavoritePodcast favoritePodcast = await _favoritePodcastRepo.GetFavoritePodcastByIdAsync(podcastId) ?? throw new NotFoundException(FavoriteErrorCodes.FavoriteItemNotFound);
            if (appUser.Id != favoritePodcast.UserId)
            {
                throw new ConflictException(FavoriteErrorCodes.FavoriteAlreadyExists);
            }

            await _favoritePodcastRepo.DeletePodcastFromFavoriteAsync(favoritePodcast);
        }

        public async Task<List<PodcastDto>> GetAllFavoritePodcastsAsync(string userId,int? favoriteListId)
        {
            AppUser appUser = await _userManager.FindByIdAsync(userId) ?? throw new NotFoundException(UserErrorCodes.UserNotFound);
            List<FavoritePodcast> favoritePodcasts = await _favoritePodcastRepo.GetAllFavoritePodcastsAsync(userId);
            if (favoriteListId.HasValue)
            {
               favoritePodcasts = favoritePodcasts.Where(f => f.FavoriteListId == favoriteListId).ToList();
            }
            List<PodcastDto> podcastDtos = _mapper.Map<List<PodcastDto>>(favoritePodcasts.Select(f => f.Podcast));
            return podcastDtos;
        }
    }
}