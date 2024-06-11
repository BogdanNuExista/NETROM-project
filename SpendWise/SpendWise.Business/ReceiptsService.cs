using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Newtonsoft.Json;
using SpendWise.Business.Exceptions;
using SpendWise.Business.Interfaces;
using SpendWise_DataAccess.Dtos;
using SpendWise_DataAccess.Entities;
using SpendWise_DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SpendWise.Business
{
    public class ReceiptsService : IReceiptsService
    {
        private string OCRurl = "https://netrom-ligaaclabs2024-dev.stage04.netromsoftware.ro";

        private const string ChatGptUrl = "https://netrom-ligaaclabs2024chatgpt-dev.stage04.netromsoftware.ro/";

        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<Cart> _cartRepository;
        public ReceiptsService(IRepository<Product> productRepository, IRepository<Category> categoryRepository, IRepository<Cart> cartRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _cartRepository = cartRepository;
        }

        public async Task<List<CategorisedProductsDto>> ScanReceipt(List<Category> categories, IFormFile image)
        {
            var imageOcr = await GetImageOcr(image);

            var categoriesProducts = await getCategoriesProducts(categories, imageOcr);

            var deserializeCategoriesProducts = JsonConvert.DeserializeObject<string>(categoriesProducts);
            if(deserializeCategoriesProducts == null)
            {
                return new List<CategorisedProductsDto>();
            }

            var deserializeProducts = JsonConvert.DeserializeObject<Dictionary<string, List<ScannedProducts>>>(deserializeCategoriesProducts);
            if(deserializeProducts == null)
            {
                return new List<CategorisedProductsDto>();
            }

            var categorisedProductsDto = new List<CategorisedProductsDto>();

            foreach(var category in deserializeProducts)
            {
                var products = category.Value.DistinctBy(p => new { p.Name, p.Price }).ToList();

                foreach(var product in products)
                {
                    product.Quantity = category.Value.Where(p => p.Name == product.Name && p.Price == product.Price).
                        Sum(p => p.Quantity);
                }

                categorisedProductsDto.Add(new CategorisedProductsDto
                {
                    Id = categories.First(c => c.Name == category.Key).Id,
                    Name = category.Key,
                    Products = products
                });
            }

            return categorisedProductsDto;
        }


        ///////////////////////////////////////////////////
        public async Task<Cart> SaveCart(CartCreateDto cartDto)
        {
            var repoProducts = await _productRepository.GetAllAsync();

            var cart = new Cart
            {
                Date = cartDto.Date,
            };

            var cartProducts = new List<CartProduct>();

            foreach (var categoryProduct in cartDto.CategoryProducts)
            {
                var category = await _categoryRepository.FindByIdAsync(categoryProduct.Id);

                if(category == null)
                {
                    throw new NotFoundException($"Entity of type {typeof(Category)} not found");
                }

                cartProducts.AddRange(await AddProduct(category, categoryProduct.Products, repoProducts.ToList()));
            }

            cart.CartProducts = cartProducts;

            await _cartRepository.PostAsync(cart);

            return cart;

        }









        #region Private Methods

        private async Task<string> GetImageOcr(IFormFile image)
        {
            string imageOcr = string.Empty;
            using var ms = new MemoryStream();
            await image.CopyToAsync(ms);
            var bytes = ms.ToArray();

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(OCRurl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var content = new MultipartFormDataContent();
            content.Add(new ByteArrayContent(bytes), "image", "image.jpg");

            var uri = new Uri(OCRurl);

            var response = client.PostAsync(uri, content).Result;

            if(response.IsSuccessStatusCode)
            {
                imageOcr = await response.Content.ReadAsStringAsync();
            }
            
            await ms.DisposeAsync();
            client.Dispose();

            return imageOcr;
        }

        private async Task<string> getCategoriesProducts(List<Category> categories, string imageOCR)
        {
            var categoriesProducts = string.Empty;
            HttpClient client = new HttpClient();

            var uri = new Uri(ChatGptUrl);
            client.BaseAddress = uri;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var content = new MultipartFormDataContent();
            content.Add(new StringContent(imageOCR), "ocr");
            content.Add(new StringContent(JsonConvert.SerializeObject(categories)), "categories");
            

            var response = client.PostAsync(uri, content).Result;

            if(response.IsSuccessStatusCode)
            {
                categoriesProducts = await response.Content.ReadAsStringAsync();
            }

            client.Dispose();

            return categoriesProducts;

        }

        private async Task<List<CartProduct>> AddProduct(Category category, List<ScannedProducts> scannedProducts, List<Product> repoProducts)
        { 
            var cartProducts = new List<CartProduct>();

            foreach (var scannedProduct in scannedProducts)
            {
                var product = repoProducts.FirstOrDefault(p => p.Name == scannedProduct.Name);

                if (product == null)
                {
                    product = new Product
                    {
                        Name = scannedProduct.Name,
                        Categories = new List<Category> { category }
                    };

                    await _productRepository.PostAsync(product);

                    var cartProduct = new CartProduct
                    {
                        Product = product,
                        Quantity = scannedProduct.Quantity,
                        Price = scannedProduct.Price
                    };

                    cartProducts.Add(cartProduct);
                }
                else
                {
                    if (!product.Categories.Contains(category))
                    {
                        product.Categories.Add(category);

                        await _productRepository.UpdateAsync(product);
                    }

                    var cartProduct = new CartProduct
                    {
                        Product = product,
                        Quantity = scannedProduct.Quantity,
                        Price = scannedProduct.Price,
                    };

                    cartProducts.Add(cartProduct);
                }
            }

            return cartProducts;

        }

        #endregion
    }
}
