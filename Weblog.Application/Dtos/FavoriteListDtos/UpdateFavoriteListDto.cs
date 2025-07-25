using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Weblog.Application.Dtos.FavoriteListDtos
{
    public class UpdateFavoriteListDto
    {
        [MaxLength(15 , ErrorMessage = "Name can not be more than 15 char")]
        public required string Name { get; set; }
        [MaxLength(50 , ErrorMessage = "Description can not be more than 15 char")]
        public required string Description { get; set; }
    }
}