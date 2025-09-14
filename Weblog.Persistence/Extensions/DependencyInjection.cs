using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Weblog.Application.Interfaces.Repositories;
using Weblog.Persistence.Repositories;

namespace Weblog.Persistence.Extensions
{
    public static class DependencyInjection
    {
        public static void AddPersistence(this IServiceCollection services)
        {
            services.AddScoped<IArticleRepository, ArticleRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped<IContributorRepository, ContributorRepository>();
            services.AddScoped<IMediumRepository, MediumRepository>();
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<IPodcastRepository, PodcastRepository>();
            services.AddScoped<IVerificationCodeRepository, VerificationCodeRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IFavoriteArticleRepository, FavoriteArticleRepository>();
            services.AddScoped<IFavoriteEventRepository, FavoriteEventRepository>();
            services.AddScoped<IFavoritePodcastRepository, FavoritePodcastRepository>();
            services.AddScoped<ITakingPartRepository, TakingPartRepository>();
            services.AddScoped<IFavoriteListRepository, FavoriteListRepository>();
            services.AddScoped<ILikeContentRepository, LikeContentRepository>();
            services.AddScoped<IUserProfileRepository, UserProfileRepository>();
        }
    }
}