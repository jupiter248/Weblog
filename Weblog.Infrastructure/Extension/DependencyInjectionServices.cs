using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Weblog.Application.Common.Interfaces;
using Weblog.Application.Interfaces;
using Weblog.Application.Interfaces.Repositories;
using Weblog.Application.Interfaces.Services;
using Weblog.Infrastructure.Localization;
using Weblog.Infrastructure.Services;

namespace Weblog.Infrastructure.Extension
{
    public static class DependencyInjectionServices
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {

            //Services
            services.AddSingleton<IErrorService, ErrorService>();
            
            services.AddScoped<IArticleService, ArticleService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ITagService, TagService>();
            services.AddScoped<IContributorService, ContributorService>();
            services.AddScoped<IMediumService, MediumService>();
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<IPodcastService, PodcastService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ISmsService, SmsService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IFavoriteArticleService, FavoriteArticleService>();
            services.AddScoped<IFavoriteEventService, FavoriteEventService>();
            services.AddScoped<IFavoritePodcastService, FavoritePodcastService>();
            services.AddScoped<ITakingPartService, TakingPartService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IFavoriteListService, FavoriteListService>();
            services.AddScoped<ILikeContentService, LikeContentService>();
            // services.AddScoped<IViewContentService, ViewContentService>();
            services.AddScoped<IContentExistenceService, ContentExistenceService>();

        }
    }
}