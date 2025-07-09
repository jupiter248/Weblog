using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Weblog.Application.Interfaces;
using Weblog.Application.Interfaces.Repositories;
using Weblog.Application.Interfaces.Services;
using Weblog.Domain.Models;
using Weblog.Infrastructure.Services;
using Weblog.Persistence.Repositories;

namespace Weblog.Infrastructure.Extension
{
    public static class DependencyInjectionServices
    {
        public static void ApplyDependencies(this IServiceCollection services)
        {
            //Repositories
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









            //Services
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






        }
    }
}