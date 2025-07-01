using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Weblog.Domain.Enums;

namespace Weblog.Application.Dtos.CategoryDtos
{
    public class UpdateCategoryDto
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public required EntityType EntityType { get; set; }
    }
}