using SpendWise.Business.Interfaces;
using SpendWise_DataAccess.Dtos;
using SpendWise_DataAccess.Entities;
using SpendWise_DataAccess.Repositories;

namespace SpendWise.Business
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Category> _categoryRepository;

        public ProductService(IRepository<Product> productRepository, IRepository<Category> categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<ShowProductDto>> GetProductsAsync()
        {
            var products = await _productRepository.GetAllAsync();
            var productsDtos = new List<ShowProductDto>();
            foreach (var product in products)
            {
                productsDtos.Add(new ShowProductDto
                {
                    Id = product.Id,
                    Name = product.Name,
                    CategoryNames = product.Categories.Select(c => c.Name)
                });
            }
            return productsDtos;
        } 

        public async Task<ShowProductDto?> GetProductByIdAsync(int id)
        {
            return await _productRepository.FindByIdAsync(id) switch
            {
                null => null,
                var product => new ShowProductDto
                {
                    Id = product.Id,
                    Name = product.Name,
                    CategoryNames = product.Categories.Select(c => c.Name)
                }
            };
        }

        public async Task<ProductDto> PostProductAsync(ProductDto productDto)
        {
            var product = new Product
            {
                Name = productDto.Name,
                Categories = new List<Category>()
            };

            foreach (var categoryId in productDto.CategoryId)
            {
                var category = await _categoryRepository.FindByIdAsync(categoryId);
                if (category != null)
                {
                    product.Categories.Add(category);
                }
            }

            var createdProduct = await _productRepository.PostAsync(product);

            return new ProductDto
            {
                Name = createdProduct.Name,
                CategoryId = createdProduct.Categories.Select(c => c.Id).ToList()
            };
        }

        public async Task<ProductDto?> UpdateProductAsync(int id, ProductDto productDto)
        {
            var product = await _productRepository.FindByIdAsync(id);
            if (product == null)
            {
                return null;
            }

            product.Name = productDto.Name;
            
            product.Categories.Clear();

            foreach (var categoryId in productDto.CategoryId)
            {
                var category = await _categoryRepository.FindByIdAsync(categoryId);
                if (category != null)
                {
                    product.Categories.Add(category);
                }
            }

            await _productRepository.UpdateAsync(product);

            return new ProductDto
            {
                Name = product.Name,
                CategoryId = product.Categories.Select(c => c.Id).ToList()
            };
        }


        public async Task<ProductDto?> DeleteProductAsync(int id)
        {
            var product = await _productRepository.FindByIdAsync(id);
            if (product == null)
            {
                return null;
            }

            await _productRepository.DeleteAsync(id);

            return new ProductDto
            {
                Name = product.Name,
                CategoryId = product.Categories.Select(c => c.Id).ToList()
            };
        }



    }
}
