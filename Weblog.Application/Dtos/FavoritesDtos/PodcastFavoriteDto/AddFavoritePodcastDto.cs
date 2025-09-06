using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblog.Application.Dtos.FavoritesDtos.PodcastFavoriteDto
{
    public class AddFavoritePodcastDto
    {
        public int PodcastId { get; set; }
        public int? FavoriteListId { get; set; }
    }
}