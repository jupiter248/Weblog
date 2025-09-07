using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblog.Application.Dtos.LikeContentDtos;
using Weblog.Application.Dtos.UserDtos;
using Weblog.Application.Interfaces.Repositories;
using Weblog.Domain.Enums;
using Weblog.Domain.JoinModels;

namespace Weblog.Application.Interfaces.Services
{
    public interface ILikeContentService
    {
        Task<List<UserDto>> GetAllContentLikesAsync(int entityTypeId, LikeAndViewType entityType);
        Task LikeAsync(string userId, LikeContentDto likeContentDto);
        Task UnlikeAsync(string userId, UnLikeContentDto unLikeContentDto);
        Task<bool> IsLikedAsync(string userId, int entityTypeId, LikeAndViewType entityType);
        Task<int> GetLikeCountAsync( int entityTypeId, LikeAndViewType entityType);
   }
}