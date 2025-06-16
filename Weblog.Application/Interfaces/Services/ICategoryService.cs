using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblog.Application.Dtos.CategoryDtos;
using Weblog.Application.Queries;

namespace Weblog.Application.Interfaces.Services
{
    public interface ICategoryService
    {
        Task<List<CategoryDto>> GetAllCategoriesAsync(FilteringCategoryParams filteringCategoryParams);
        Task<CategoryDto> GetCategoryByIdAsync(int CategoryId);
        Task<CategoryDto> AddCategoryAsync(AddCategoryDto addCategoryDto);
        Task UpdateCategoryAsync(UpdateCategoryDto updateCategoryDto , int categoryId);
        Task DeleteCategoryAsync(int CategoryId);
    }
}