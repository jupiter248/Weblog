using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblog.Domain.Enums;
using Weblog.Domain.JoinModels;

namespace Weblog.Application.Interfaces.Repositories
{
    public interface ILikeContentRepository
    {
        Task<List<LikeContent>> GetAllLikeContentAsync(string UserId);
        Task<List<LikeContent>> GetAllContentLikesAsync(int entityTypeId , LikeAndViewType entityType);
        Task<LikeContent?> GetLikeContentByIdAsync(int likeContentId);
        Task<int> GetLikeCountAsync(int entityTypeId , LikeAndViewType entityType);
        Task<LikeContent> AddLikeContentAsync(LikeContent likeContent);
        Task DeleteLikeContentAsync(LikeContent likeContent); 
    }
}