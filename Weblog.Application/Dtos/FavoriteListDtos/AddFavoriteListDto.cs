using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblog.Domain.Enums;

namespace Weblog.Application.Dtos.FavoriteListDtos
{
    public class AddFavoriteListDto
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required FavoriteListType EntityType { get; set; }
    }
}