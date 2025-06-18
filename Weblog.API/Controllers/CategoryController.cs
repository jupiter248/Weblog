using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Weblog.Application.Dtos.CategoryDtos;
using Weblog.Application.Interfaces.Services;
using Weblog.Application.Queries;
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
        public async Task<IActionResult> GetAllCategories([FromQuery] FilteringCategoryParams filteringCategoryParams)
        {
            List<CategoryDto> categoryDtos = await _categoryService.GetAllCategoriesAsync(filteringCategoryParams);
            return Ok(categoryDtos);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCategoryById([FromRoute] int id)
        {
            CategoryDto categoryDto = await _categoryService.GetCategoryByIdAsync(id);
            return Ok(categoryDto);
        }
        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] AddCategoryDto addCategoryDto)
        {
            CategoryDto categoryDto = await _categoryService.AddCategoryAsync(addCategoryDto);
            return CreatedAtAction(nameof(GetCategoryById), new { id = categoryDto.Id }, categoryDto);
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateCategory([FromRoute] int id, [FromBody] UpdateCategoryDto updateCategoryDto)
        {
            await _categoryService.UpdateCategoryAsync(updateCategoryDto, id);
            return NoContent();
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] int id)
        {
            await _categoryService.DeleteCategoryAsync(id);
            return NoContent();
        }
        
    }
}