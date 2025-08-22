using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Weblog.Application.Dtos.FavoriteListDtos
{
    public class UpdateFavoriteListDto
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
    }
}