using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Weblog.Application.CustomExceptions;
using Weblog.Application.Dtos.CategoryDtos;
using Weblog.Application.Interfaces.Repositories;
using Weblog.Application.Interfaces.Services;
using Weblog.Application.Queries;
using Weblog.Application.Queries.FilteringParams;
using Weblog.Domain.Errors.Category;
using Weblog.Domain.Models;

namespace Weblog.Infrastructure.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepo;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepo, IMapper mapper)
        {
            _categoryRepo = categoryRepo;
            _mapper = mapper;
        }
        public async Task<CategoryDto> AddCategoryAsync(AddCategoryDto addCategoryDto)
        {
            Category newCategory = _mapper.Map<Category>(addCategoryDto);
            Category addedCategory = await _categoryRepo.AddCategoryAsync(newCategory);
            return _mapper.Map<CategoryDto>(addedCategory);
        }

        public async Task DeleteCategoryAsync(int categoryId)
        {
            Category? category = await _categoryRepo.GetCategoryByIdAsync(categoryId) ?? throw new NotFoundException(CategoryErrorCodes.CategoryNotFound);
            await _categoryRepo.DeleteCategoryAsync(category);
        }

        public async Task<List<CategoryDto>> GetAllCategoriesAsync(CategoryFilteringParams categoryFilteringParams)
        {
            List<Category> categories = await _categoryRepo.GetAllCategoriesAsync(categoryFilteringParams);
            List<CategoryDto> categoryDtos = _mapper.Map<List<CategoryDto>>(categories);
            return categoryDtos;
        }

        public async Task<CategoryDto> GetCategoryByIdAsync(int categoryId)
        {
            Category? category = await _categoryRepo.GetCategoryByIdAsync(categoryId) ?? throw new NotFoundException(CategoryErrorCodes.CategoryNotFound);
            return _mapper.Map<CategoryDto>(category);

        }

        public async Task<CategoryDto> UpdateCategoryAsync(UpdateCategoryDto updateCategoryDto, int categoryId)
        {
            Category? currentCategory = await _categoryRepo.GetCategoryByIdAsync(categoryId) ?? throw new NotFoundException(CategoryErrorCodes.CategoryNotFound);
            currentCategory = _mapper.Map(updateCategoryDto, currentCategory);
            await _categoryRepo.UpdateCategoryAsync(currentCategory);
            return _mapper.Map<CategoryDto>(currentCategory);
        }
    }
}