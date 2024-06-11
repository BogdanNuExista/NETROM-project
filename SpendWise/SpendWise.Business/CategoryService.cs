using Microsoft.EntityFrameworkCore.Query.Internal;
using SpendWise.Business.Interfaces;
using SpendWise_DataAccess.Dtos;
using SpendWise_DataAccess.Entities;
using SpendWise_DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpendWise.Business
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> _categoryRepository;

        public CategoryService(IRepository<Category> _categoryRepository, IRepository<Product> _productRepository) {
            this._categoryRepository = _categoryRepository;
        }

        public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();
            var categoriesDtos = new List<CategoryDto>();

            categoriesDtos = categories.Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name,
            }).ToList();

            return categoriesDtos;
        }

        public async Task<List<Category>> GetAlCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();

            
            return categories.ToList();
        }

        public async Task<List<TotalPriceCategoryDto>> GetTotalPriceCategoriesAsync(DateTime dateFrom, DateTime dateTo)
        {
            var categories = await _categoryRepository.GetAllAsync();

            var totalPriceCategoryDto = new List<TotalPriceCategoryDto>();

            foreach (var category in categories)
            {
                var totalPrice = 0.0;

                foreach (var product in category.Products)
                {

                    totalPrice += product.CartProducts.Where(cp => cp.Cart.Date>=dateFrom && cp.Cart.Date<=dateTo).Sum(cp => cp.Quantity * cp.Price);
                }

                var totalPriceCategory = new TotalPriceCategoryDto
                {
                    Id = category.Id,
                    Name = category.Name,
                    TotalPrice = totalPrice
                };

                totalPriceCategoryDto.Add(totalPriceCategory);
            }

            return totalPriceCategoryDto;
        }

        public async Task<CategoryDto> PostCategoryAsync(CategoryDto categoryDto)
        {
            var category = new Category
            {
                Name = categoryDto.Name,
                Products = new List<Product>()
            };

            var createdCategory = await _categoryRepository.PostAsync(category);

            return new CategoryDto
            {
                Name = createdCategory.Name,
            };
        }


        public async Task<CategoryDto?> GetCategoryByIdAsync(int id)
        {
            return await _categoryRepository.FindByIdAsync(id) switch
            {
                null => null,
                var category => new CategoryDto
                {
                    Id = category.Id,
                    Name = category.Name,
                }
            };  
        }

        public async Task<CategoryDto?> UpdateCategoryAsync(int id, CategoryDto categoryDto)
        {
            var category = await _categoryRepository.FindByIdAsync(id);
            if(category == null)
            {
                return null;
            }
            category.Name = categoryDto.Name;

            await _categoryRepository.UpdateAsync(category);
            return new CategoryDto
            {
                Name = category.Name,
            };
            
        }

        public async Task<CategoryDto?> DeleteCategoryAsync(int id)
        {
            var category = await _categoryRepository.FindByIdAsync(id);
            if(category == null)
            {
                return null;
            }

            await _categoryRepository.DeleteAsync(id);

            return new CategoryDto
            {

                Name = category.Name,
            };
        } 
        
    }
}
