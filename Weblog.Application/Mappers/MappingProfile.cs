using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Weblog.Application.Dtos;
using Weblog.Domain.Models;

namespace Weblog.Application.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Article, ArticleDto>();
            CreateMap<ArticleDto, AddArticleDto>();
            CreateMap<Article, UpdateArticleDto>();

        }
    }
}