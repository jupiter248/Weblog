using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblog.Application.Dtos.CategoryDtos;
using Weblog.Application.Interfaces.Repositories;
using Weblog.Application.Interfaces.Services;
using Weblog.Application.Queries;

namespace Weblog.Infrastructure.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepo;
        public CategoryService(ICategoryRepository categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }
        public Task<CategoryDto> AddCategoryAsync(AddCategoryDto addCategoryDto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteCategoryAsync(int CategoryId)
        {
            throw new NotImplementedException();
        }

        public Task<List<CategoryDto>> GetAllCategoriesAsync(FilteringCategoryParams filteringCategoryParams)
        {
            throw new NotImplementedException();
        }

        public Task<CategoryDto> GetCategoryByIdAsync(int CategoryId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateCategoryAsync(UpdateCategoryDto updateCategoryDto)
        {
            throw new NotImplementedException();
        }
    }
}