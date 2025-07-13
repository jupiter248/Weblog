using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Weblog.Application.Dtos.ArticleDtos;
using Weblog.Application.Dtos.EventDtos;
using Weblog.Application.Dtos.PodcastDtos;
using Weblog.Domain.Enums;

namespace Weblog.Application.Dtos.FavoriteListDtos
{
    public class FavoriteListDto
    {
        [Key]
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required FavoriteListType EntityType { get; set; }
        public required DateTimeOffset CreatedAt { get; set; }
        public required DateTimeOffset UpdatedAt { get; set; }
        public List<ArticleDto>? Articles { get; set; }
        public List<EventDto>? Events { get; set; }
        public List<PodcastDto>? Podcasts { get; set; }
    }
}