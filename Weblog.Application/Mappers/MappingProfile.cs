using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Weblog.Application.Dtos;
using Weblog.Application.Dtos.ArticleDtos;
using Weblog.Application.Dtos.CategoryDtos;
using Weblog.Domain.Models;

namespace Weblog.Application.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Article
            CreateMap<Article, ArticleDto>();
            CreateMap<AddArticleDto, Article>();
            CreateMap<UpdateArticleDto, Article>();
            //Category
            CreateMap<Category, CategoryDto>();
            CreateMap<AddCategoryDto, Category>();
            CreateMap<UpdateCategoryDto, Category>();
        }
    }
}