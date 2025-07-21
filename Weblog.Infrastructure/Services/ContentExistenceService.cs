using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblog.Application.Interfaces.Repositories;
using Weblog.Application.Interfaces.Services;
using Weblog.Domain.Enums;

namespace Weblog.Infrastructure.Services
{
    public class ContentExistenceService : IContentExistenceService
    {
        private readonly IPodcastRepository _podcastRepo;
        private readonly IArticleRepository _articleRepo;
        private readonly IEventRepository _eventRepo;

        public ContentExistenceService(
            IArticleRepository articleRepository, IPodcastRepository podcastRepository, IEventRepository eventRepository)
        {
            _articleRepo = articleRepository;
            _eventRepo = eventRepository;
            _podcastRepo = podcastRepository;
        }

        public async Task<bool> ContentExistsAsync(int contentId, LikeAndViewType contentType)
        {
            return contentType switch
            {
                LikeAndViewType.Article => await _articleRepo.ArticleExistsAsync(contentId),
                LikeAndViewType.Event => await _eventRepo.EventExistsAsync(contentId),
                LikeAndViewType.Podcast => await _podcastRepo.PodcastExistsAsync(contentId),
                _ => false
            };
        }
    }
}