using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopingList.Services
{
    using Common.Contracts.DataContracts;
    using Common.Contracts.ServiceContracts;

    using Data.Sql.Repositories;

    public class ProductService : IProductService
    {
        private readonly ProductRepository _productRepository;

        public ProductService()
        {
            _productRepository= new ProductRepository();
        }

        public async Task<Product> GetProductAsync(Guid productId)
        {
            return await _productRepository.GetProductAsync(productId);
        }

        public async Task<List<Product>> GetProductsByCategoryAsync(Guid categoryId)
        {
            return await _productRepository.GetProductsByCategoryAsync(categoryId);
        }

        public async Task<List<Product>> SearchProductsAsync(string name)
        {
            return await _productRepository.SearchProductsAsync(name);
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _productRepository.GetAllProductsAsync();
        }

        public async Task<Guid> AddProductAsync(Product product)
        {
           return await _productRepository.AddProductAsync(product);
        }

        public async Task UpdateProductAsync(Product product)
        {
            await _productRepository.UpdateProductAsync(product);
        }

        public async Task DeleteProductAsync(Product product)
        {
            await _productRepository.DeleteProductAsync(product);
        }
    }
}
