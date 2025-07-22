using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblog.Domain.Enums;
using Weblog.Domain.JoinModels;
using Weblog.Domain.Models;

namespace Weblog.Application.Interfaces.Repositories
{
    public interface IViewContentRepository
    {
        Task ViewAsync(AppUser appUser, int entityTypeId, LikeAndViewType entityType);
        Task<int> GetViewCountAsync(int entityTypeId, LikeAndViewType entityType);
        Task<bool> IsViewedAsync(string userId, int entityTypeId, LikeAndViewType entityType);

    }
}