using Microsoft.AspNetCore.Mvc;
using SpendWise.Business.Interfaces;
using SpendWise_DataAccess.Dtos;
using SpendWise_DataAccess.Entities;
using SpendWise_DataAccess.Repositories;
using System.ComponentModel;

namespace SpendWise.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        /////////////////////////////////////////
        [HttpGet("GetPriceForCategories")]
        public async Task<List<TotalPriceCategoryDto>> GetPriceForCategories([FromQuery] DateTime dateFrom, [FromQuery] DateTime dateTo)
        {
            var categories = await _categoryService.GetTotalPriceCategoriesAsync(dateFrom, dateTo);
            return categories;
        }




        [HttpGet("GetCategories")]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _categoryService.GetCategoriesAsync();
            return Ok(categories);
        }

        [HttpGet("GetCategory/{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if(category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpPost("CreateCategory")]
        public async Task<IActionResult> AddCategory([FromBody] CategoryDto category)
        {
            var newCategory = await _categoryService.PostCategoryAsync(category);
            return Ok(newCategory);
        }

        [HttpPatch("UpdateCategory/{id}")] 
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryDto category)
        {
            var updatedCategory = await _categoryService.UpdateCategoryAsync(id, category);
            if(updatedCategory == null)
            {
                return NotFound();
            }
            return Ok(updatedCategory);
        }

        [HttpDelete("DeleteCategory/{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var deletedCategory = await _categoryService.DeleteCategoryAsync(id);
            if (deletedCategory == null)
            {
                return NotFound();
            }
            return Ok(deletedCategory);
        }

    }
}
