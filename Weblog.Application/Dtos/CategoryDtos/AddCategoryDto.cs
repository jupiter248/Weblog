using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Weblog.Domain.Enums;

namespace Weblog.Application.Dtos.CategoryDtos
{
    public class AddCategoryDto
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public CategoryType? EntityType { get; set; }
    }
}