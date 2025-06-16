using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblog.Application.Queries;
using Weblog.Domain.Models;

namespace Weblog.Application.Interfaces.Repositories
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllCategoriesAsync(FilteringCategoryParams filteringCategoryParams);
        Task<Category> AddCategoryAsync(Category category);
        Task<Category> GetCategoryByIdAsync(int CategoryId);
        Task UpdateCategoryAsync(Category currentCategory, Category newCategory);
        Task DeleteCategoryAsync(Category category);    
    }
}