using SpendWise_DataAccess.Dtos;
using SpendWise_DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SpendWise.Business.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetCategoriesAsync();

        Task<CategoryDto?> GetCategoryByIdAsync(int id);

        Task<CategoryDto> PostCategoryAsync(CategoryDto categoryDto);

        Task<CategoryDto?> UpdateCategoryAsync(int id, CategoryDto categoryDto);

        Task<CategoryDto?> DeleteCategoryAsync(int id);

        Task<List<Category>> GetAlCategoriesAsync();

        Task<List<TotalPriceCategoryDto>> GetTotalPriceCategoriesAsync(DateTime dateFrom, DateTime dateTo);

    }
}
