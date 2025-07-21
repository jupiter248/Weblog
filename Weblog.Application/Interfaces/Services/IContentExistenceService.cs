using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblog.Domain.Enums;

namespace Weblog.Application.Interfaces.Services
{
    public interface IContentExistenceService
    {
        Task<bool> ContentExistsAsync(int contentId, LikeAndViewType contentType);
    }
}