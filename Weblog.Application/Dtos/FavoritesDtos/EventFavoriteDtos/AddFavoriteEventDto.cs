using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblog.Application.Dtos.FavoritesDtos.EventFavoriteDtos
{
    public class AddFavoriteEventDto
    {
        public int EventId { get; set; }
        public int? FavoriteListId { get; set; }
    }
}