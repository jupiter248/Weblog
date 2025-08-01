using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblog.Domain.Enums;
using Weblog.Domain.JoinModels;
using Weblog.Domain.Models;

namespace Weblog.Application.Interfaces.Repositories
{
    public interface ILikeContentRepository
    {
        Task<List<LikeContent>> GetAllLikeContentAsync(string UserId);
        Task<List<LikeContent>> GetAllContentLikesAsync(int entityTypeId, LikeAndViewType entityType);
        Task<int> GetLikeCountAsync(int entityTypeId, LikeAndViewType entityType);
        Task LikeAsync(AppUser appUser, int entityTypeId, LikeAndViewType entityType);
        Task UnlikeAsync(string userId, int entityTypeId, LikeAndViewType entityType);
        Task<bool> IsLikedAsync(string userId, int entityTypeId, LikeAndViewType entityType);

    }
}