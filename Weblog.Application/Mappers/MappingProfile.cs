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
using Weblog.Application.Dtos.MediaDtos;
using Weblog.Application.Dtos.PodcastDtos;
using Weblog.Application.Dtos.TagDtos;
using Weblog.Application.Dtos.UserDtos;
using Weblog.Domain.JoinModels;
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
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));
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
            CreateMap<Contributor, ContributorDto>();
            CreateMap<AddContributorDto, Contributor>();
            CreateMap<UpdateContributorDto, Contributor>();
            //Medium
            CreateMap<Medium, MediumDto>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId));
            CreateMap<UpdateMediumDto, Medium>();
            //Event
            CreateMap<Event, EventDto>()
                .ForMember(dest => dest.TagDtos, opt => opt.MapFrom(src => src.Tags))
                .ForMember(dest => dest.MediumDtos, opt => opt.MapFrom(src => src.Media))
                .ForMember(dest => dest.ContributorDtos, opt => opt.MapFrom(src => src.Contributors))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));
            CreateMap<AddEventDto, Event>();
            CreateMap<UpdateEventDto, Event>();
            //Podcast
            CreateMap<Podcast, PodcastDto>()
                .ForMember(dest => dest.TagDtos, opt => opt.MapFrom(src => src.Tags))
                .ForMember(dest => dest.ContributorDtos, opt => opt.MapFrom(src => src.Contributors))
                .ForMember(dest => dest.MediumDtos, opt => opt.MapFrom(src => src.Media))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));
            CreateMap<AddPodcastDto, Podcast>();
            CreateMap<UpdatePodcastDto, Podcast>();
            //Comment
            CreateMap<Comment, CommentDto>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.AppUser.FullName));
            CreateMap<AddCommentDto, Comment>();
            CreateMap<UpdateCommentDto, Comment>();
            //User
            CreateMap<UpdateUserDto, AppUser>();
            CreateMap<AppUser, UserDto>()
                .ForMember(dest => dest.Media, opt => opt.MapFrom(src => src.Media));

        }
    }
}