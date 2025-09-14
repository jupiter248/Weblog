using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Weblog.Application.Dtos;
using Weblog.Application.Dtos.ArticleDtos;
using Weblog.Application.Dtos.CategoryDtos;
using Weblog.Application.Dtos.CommentDtos;
using Weblog.Application.Dtos.ContributorDtos;
using Weblog.Application.Dtos.EventDtos;
using Weblog.Application.Dtos.FavoriteListDtos;
using Weblog.Application.Dtos.MediaDtos;
using Weblog.Application.Dtos.PodcastDtos;
using Weblog.Application.Dtos.TagDtos;
using Weblog.Application.Dtos.UserDtos;
using Weblog.Application.Dtos.UserProfileDtos;
using Weblog.Application.Helpers;
using Weblog.Domain.JoinModels;
using Weblog.Domain.JoinModels.Favorites;
using Weblog.Domain.Models;

namespace Weblog.Application.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Article
            CreateMap<Article, ArticleDto>()
                .ForMember(dest => dest.TagDtos, opt => opt.MapFrom(src => src.Tags))
                .ForMember(dest => dest.ContributorDtos, opt => opt.MapFrom(src => src.Contributors))
                .ForMember(dest => dest.MediumDtos, opt => opt.MapFrom(src => src.Media))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToShamsi()))
                .ForMember(dest => dest.PublishedAt, opt => opt.MapFrom(src => src.PublishedAt.ToShamsi()))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt.ToShamsi()));

            CreateMap<Article, ArticleSummaryDto>()
                .ForMember(dest => dest.ContributorDtos, opt => opt.MapFrom(src => src.Contributors))
                .ForMember(dest => dest.MediumDtos, opt => opt.MapFrom(src => src.Media))
                .ForMember(dest => dest.PublishedAt, opt => opt.MapFrom(src => src.PublishedAt.ToShamsi()));

            CreateMap<AddArticleDto, Article>();
            CreateMap<UpdateArticleDto, Article>();
            // Category
            CreateMap<Category, CategoryDto>();
            CreateMap<AddCategoryDto, Category>();
            CreateMap<UpdateCategoryDto, Category>();
            // Tag
            CreateMap<Tag, TagDto>();
            CreateMap<AddTagDto, Tag>();
            CreateMap<UpdateTagDto, Tag>();
            // Contributor
            CreateMap<Contributor, ContributorDto>()
                .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(src => src.CreatedOn.ToShamsi()))
                .ForMember(dest => dest.MediumDtos, opt => opt.MapFrom(src => src.Media));

            CreateMap<AddContributorDto, Contributor>();
            CreateMap<UpdateContributorDto, Contributor>();
            //Medium
            CreateMap<Medium, MediumDto>();
            CreateMap<UpdateMediumDto, Medium>();
            //Event
            CreateMap<Event, EventDto>()
                .ForMember(dest => dest.TagDtos, opt => opt.MapFrom(src => src.Tags))
                .ForMember(dest => dest.MediumDtos, opt => opt.MapFrom(src => src.Media))
                .ForMember(dest => dest.ContributorDtos, opt => opt.MapFrom(src => src.Contributors))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToShamsi()))
                .ForMember(dest => dest.DisplayedAt, opt => opt.MapFrom(src => src.DisplayedAt.ToShamsi()))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt.ToShamsi()))
                .ForMember(dest => dest.StartedAt, opt => opt.MapFrom(src => src.StartedAt.ToShamsi()))
                .ForMember(dest => dest.FinishedAt, opt => opt.MapFrom(src => src.FinishedAt.ToShamsi()));
            CreateMap<Event, EventSummaryDto>()
                .ForMember(dest => dest.ContributorDtos, opt => opt.MapFrom(src => src.Contributors))
                .ForMember(dest => dest.MediumDtos, opt => opt.MapFrom(src => src.Media))
                .ForMember(dest => dest.DisplayedAt, opt => opt.MapFrom(src => src.DisplayedAt.ToShamsi()));
            CreateMap<AddEventDto, Event>();
            CreateMap<UpdateEventDto, Event>();
            //Podcast
            CreateMap<Podcast, PodcastDto>()
                .ForMember(dest => dest.TagDtos, opt => opt.MapFrom(src => src.Tags))
                .ForMember(dest => dest.ContributorDtos, opt => opt.MapFrom(src => src.Contributors))
                .ForMember(dest => dest.MediumDtos, opt => opt.MapFrom(src => src.Media))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt.ToShamsi()))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToShamsi()))
                .ForMember(dest => dest.DisplayedAt, opt => opt.MapFrom(src => src.DisplayedAt.ToShamsi()));
            CreateMap<Podcast, PodcastSummaryDto>()
                .ForMember(dest => dest.ContributorDtos, opt => opt.MapFrom(src => src.Contributors))
                .ForMember(dest => dest.MediumDtos, opt => opt.MapFrom(src => src.Media))
                .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(src => src.CreatedAt.ToShamsi()));

            CreateMap<AddPodcastDto, Podcast>();
            CreateMap<UpdatePodcastDto, Podcast>();
            //Comment
            CreateMap<Comment, CommentDto>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.AppUser.FullName))
                .ForMember(dest => dest.TextedOn, opt => opt.MapFrom(src => src.TextedOn.ToShamsi()));
            CreateMap<AddCommentDto, Comment>();
            CreateMap<UpdateCommentDto, Comment>();
            //User
            CreateMap<UpdateUserDto, AppUser>();
            CreateMap<AppUser, UserDto>()
                .ForMember(dest => dest.Profiles, opt => opt.MapFrom(src => src.UserProfiles))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToShamsi()))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt.ToShamsi()));
            //Favorite List
            CreateMap<FavoriteList, FavoriteListDto>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToShamsi()))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt.ToShamsi()));
            CreateMap<AddFavoriteListDto, FavoriteList>();
            CreateMap<UpdateFavoriteListDto, FavoriteList>();
            //User Profile
            CreateMap<UserProfile, UserProfileDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.AppUser.UserName));

        }
    }
}