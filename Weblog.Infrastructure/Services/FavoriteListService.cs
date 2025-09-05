using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Weblog.Application.CustomExceptions;
using Weblog.Application.Dtos.FavoriteListDtos;
using Weblog.Application.Interfaces.Repositories;
using Weblog.Application.Interfaces.Services;
using Weblog.Domain.Errors.Favorite;
using Weblog.Domain.Errors.User;
using Weblog.Domain.JoinModels.Favorites;
using Weblog.Domain.Models;

namespace Weblog.Infrastructure.Services
{
    public class FavoriteListService : IFavoriteListService
    {
        private readonly IFavoriteListRepository _favoriteListRepo;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;


        public FavoriteListService(IFavoriteListRepository favoriteListRepo, IMapper mapper, UserManager<AppUser> userManager)
        {
            _favoriteListRepo = favoriteListRepo;
            _mapper = mapper;
            _userManager = userManager;
        }
        public async Task<FavoriteListDto> AddFavoriteListAsync(string userId ,AddFavoriteListDto addFavoriteListDto)
        {
            AppUser appUser = await _userManager.FindByIdAsync(userId) ?? throw new NotFoundException(UserErrorCodes.UserNotFound);
            FavoriteList favoriteList = _mapper.Map<FavoriteList>(addFavoriteListDto);
            favoriteList.CreatedAt = DateTimeOffset.UtcNow;
            favoriteList.UserId = appUser.Id;
            favoriteList.AppUser = appUser;
            FavoriteList addedFavoriteList = await _favoriteListRepo.AddFavoriteListAsync(favoriteList);
            return _mapper.Map<FavoriteListDto>(addedFavoriteList);

        }

        public async Task DeleteFavoriteList( string userId,int favoriteListId)
        {
            AppUser appUser = await _userManager.FindByIdAsync(userId) ?? throw new NotFoundException(UserErrorCodes.UserNotFound);
            FavoriteList? favoriteList = await _favoriteListRepo.GetFavoriteListByIdAsync(favoriteListId) ?? throw new NotFoundException(FavoriteErrorCodes.FavoriteListNotFound);
            if (favoriteList.UserId != appUser.Id)
            {
                throw new ForbiddenException(FavoriteErrorCodes.FavoriteListDeleteForbidden, []);
            }
            await _favoriteListRepo.DeleteFavoriteListAsync(favoriteList);
        }

        public async Task<List<FavoriteListDto>> GetAllFavoriteListsAsync(string userId)
        {
            AppUser appUser = await _userManager.FindByIdAsync(userId) ?? throw new NotFoundException(UserErrorCodes.UserNotFound);
            List<FavoriteList> favoriteList = await _favoriteListRepo.GetAllFavoritesListAsync(appUser.Id);
            List<FavoriteListDto> favoriteListDtos = _mapper.Map<List<FavoriteListDto>>(favoriteList);
            return favoriteListDtos;
        }

        public async Task<FavoriteListDto> GetFavoriteListByIdAsync(int favoriteListId)
        {
            FavoriteList? favoriteList = await _favoriteListRepo.GetFavoriteListByIdAsync(favoriteListId) ?? throw new NotFoundException(FavoriteErrorCodes.FavoriteListNotFound);
            return _mapper.Map<FavoriteListDto>(favoriteList);
        }

        public async Task UpdateFavoriteListAsync(string userId , UpdateFavoriteListDto updateFavoriteListDto, int favoriteListId)
        {
            AppUser appUser = await _userManager.FindByIdAsync(userId) ?? throw new NotFoundException(UserErrorCodes.UserNotFound);
            FavoriteList? favoriteList = await _favoriteListRepo.GetFavoriteListByIdAsync(favoriteListId) ?? throw new NotFoundException(FavoriteErrorCodes.FavoriteListNotFound);
            if (favoriteList.UserId != appUser.Id)
            {
                throw new ForbiddenException(FavoriteErrorCodes.FavoriteListDeleteForbidden , []);
            }
            favoriteList.Name = updateFavoriteListDto.Name;
            favoriteList.Description = updateFavoriteListDto.Description;
            favoriteList.UpdatedAt = DateTimeOffset.Now;
            await _favoriteListRepo.UpdateFavoriteListAsync(favoriteList);
        }
    }
}