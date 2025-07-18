using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Weblog.Application.Dtos.CategoryDtos;
using Weblog.Application.Interfaces.Services;
using Weblog.Application.Queries;
using Weblog.Application.Queries.FilteringParams;
using Weblog.Domain.Models;

namespace Weblog.API.Controllers
{
    [ApiController]
    [Route("api/category")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories([FromQuery] CategoryFilteringParams categoryFilteringParams)
        {
            List<CategoryDto> categoryDtos = await _categoryService.GetAllCategoriesAsync(categoryFilteringParams);
            return Ok(categoryDtos);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCategoryById([FromRoute] int id)
        {
            CategoryDto categoryDto = await _categoryService.GetCategoryByIdAsync(id);
            return Ok(categoryDto);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddCategory([FromQuery] AddCategoryDto addCategoryDto)
        {
            CategoryDto categoryDto = await _categoryService.AddCategoryAsync(addCategoryDto);
            return CreatedAtAction(nameof(GetCategoryById), new { id = categoryDto.Id }, categoryDto);
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateCategory([FromRoute] int id, [FromBody] UpdateCategoryDto updateCategoryDto)
        {
            await _categoryService.UpdateCategoryAsync(updateCategoryDto, id);
            return NoContent();
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] int id)
        {
            await _categoryService.DeleteCategoryAsync(id);
            return NoContent();
        }
        
    }
}