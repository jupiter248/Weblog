using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Weblog.Application.Interfaces.Repositories;
using Weblog.Application.Queries;
using Weblog.Domain.Models;
using Weblog.Persistence.Data;

namespace Weblog.Persistence.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Category> AddCategoryAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task DeleteCategoryAsync(Category category)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }

        public List<Category> GetAllCategoriesAsync(FilteringCategoryParams filteringCategoryParams)
        {
            var categories = _context.Categories.AsQueryable();
            if (filteringCategoryParams.CategoryParentType.HasValue)
            {
                categories = categories.Where(t => t.CategoryParentType == filteringCategoryParams.CategoryParentType);
            }
            return categories.ToList();
        }

        public async Task<Category?> GetCategoryByIdAsync(int categoryId)
        {
            Category? category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == categoryId);
            if (category == null)
            {
                return null;
            }
            return category;
        }

        public async Task UpdateCategoryAsync(Category currentCategory, Category newCategory)
        {
            currentCategory.Name = newCategory.Name;
            currentCategory.Description = newCategory.Description;
            currentCategory.CategoryParentType = newCategory.CategoryParentType;
            await _context.SaveChangesAsync();
        }
    }
}