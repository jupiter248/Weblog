using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblog.Domain.Enums;

namespace Weblog.Application.Dtos.LikeContentDtos
{
    public class UnLikeContentDto
    {
        public int EntityTypeId { get; set; }
        public LikeAndViewType EntityType { get; set; }
    }
}