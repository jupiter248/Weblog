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
        [MaxLength(10 , ErrorMessage = "Category name can not be more than 10")]
        public required string Name { get; set; }
        [MinLength(10 , ErrorMessage = "Category name can not be less than 10")]
        public required string Description { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public required EntityType EntityType { get; set; }
    }
}