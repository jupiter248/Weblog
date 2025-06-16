using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public Task<Category> AddCategoryAsync(Category category)
        {
            throw new NotImplementedException();
        }

        public Task DeleteCategoryAsync(Category category)
        {
            throw new NotImplementedException();
        }

        public Task<List<Category>> GetAllCategoriesAsync(FilteringCategoryParams filteringCategoryParams)
        {
            throw new NotImplementedException();
        }

        public Task<Category> GetCategoryByIdAsync(int CategoryId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateCategoryAsync(Category currentCategory, Category newCategory)
        {
            throw new NotImplementedException();
        }
    }
}