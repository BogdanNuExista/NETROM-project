using SpendWise_DataAccess.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpendWise.Business.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ShowProductDto>> GetProductsAsync();

        Task<ShowProductDto?> GetProductByIdAsync(int id);

        Task<ProductDto> PostProductAsync(ProductDto productDto);

        Task<ProductDto?> UpdateProductAsync(int id, ProductDto productDto);

        Task<ProductDto?> DeleteProductAsync(int id);


    }
}
