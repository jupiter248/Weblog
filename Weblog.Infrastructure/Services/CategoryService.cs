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
            Category? category = await _categoryRepo.GetCategoryByIdAsync(categoryId) ?? throw new NotFoundException("Category not found");
            await _categoryRepo.DeleteCategoryAsync(category);
        }

        public async Task<List<CategoryDto>> GetAllCategoriesAsync(FilteringCategoryParams filteringCategoryParams)
        {
            List<Category> categories = await _categoryRepo.GetAllCategoriesAsync(filteringCategoryParams);
            List<CategoryDto> categoryDtos = _mapper.Map<List<CategoryDto>>(categories);
            return categoryDtos;
        }

        public async Task<CategoryDto> GetCategoryByIdAsync(int categoryId)
        {
            Category? category = await _categoryRepo.GetCategoryByIdAsync(categoryId) ?? throw new NotFoundException("Category not found");
            return _mapper.Map<CategoryDto>(category);

        }

        public async Task UpdateCategoryAsync(UpdateCategoryDto updateCategoryDto, int categoryId)
        {
            Category? currentCategory = await _categoryRepo.GetCategoryByIdAsync(categoryId) ?? throw new NotFoundException("Category not found");
            Category newCategory = _mapper.Map<Category>(updateCategoryDto);
            await _categoryRepo.UpdateCategoryAsync(currentCategory , newCategory);
        }
    }
}