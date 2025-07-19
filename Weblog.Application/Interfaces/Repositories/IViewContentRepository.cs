using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblog.Domain.Enums;
using Weblog.Domain.JoinModels;

namespace Weblog.Application.Interfaces.Repositories
{
    public interface IViewContentRepository
    {
        Task<ViewContent?> GetViewContentByIdAsync(int viewContentId);
        Task<ViewContent> AddViewContentAsync(ViewContent viewContent);
        Task<int> GetViewCountAsync(int entityTypeId , LikeAndViewType entityType);
        Task DeleteViewContentAsync(ViewContent viewContent); 
    }
}