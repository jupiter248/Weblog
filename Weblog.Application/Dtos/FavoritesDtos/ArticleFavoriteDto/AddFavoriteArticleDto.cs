using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblog.Application.Dtos.FavoritesDtos.ArticleFavoriteDto
{
    public class AddFavoriteArticleDto
    {
        public int ArticleId { get; set; }
        public int? favoriteListId { get; set; }
    }
}