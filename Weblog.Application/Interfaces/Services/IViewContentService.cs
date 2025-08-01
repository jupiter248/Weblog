using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblog.Application.Dtos.UserDtos;
using Weblog.Domain.Enums;
using Weblog.Domain.JoinModels;

namespace Weblog.Application.Interfaces.Services
{
    public interface IViewContentService
    {
        Task<List<UserDto>> GetAllContentViewersAsync(int entityTypeId, LikeAndViewType entityType);
        Task AddViewContentAsync(string userId, int entityTypeId, LikeAndViewType entityType);
        Task<int> GetViewCountAsync(int entityTypeId, LikeAndViewType entityType);
        Task<bool> IsViewedAsync(string userId, int entityTypeId, LikeAndViewType entityType);
    }
}