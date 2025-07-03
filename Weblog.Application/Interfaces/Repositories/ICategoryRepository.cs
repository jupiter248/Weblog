using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblog.Application.Queries;
using Weblog.Application.Queries.FilteringParams;
using Weblog.Domain.Models;

namespace Weblog.Application.Interfaces.Repositories
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllCategoriesAsync(CategoryFilteringParams categoryFilteringParams);
        Task<Category> AddCategoryAsync(Category category);
        Task<Category?> GetCategoryByIdAsync(int categoryId);
        Task UpdateCategoryAsync(Category currentCategory, Category newCategory);
        Task DeleteCategoryAsync(Category category);    
    }
}