using System;
using System.Collections.Generic;

namespace ShopingList.Common.Contracts.ServiceContracts
{
    using System.Threading.Tasks;
    using DataContracts;

    public interface IProductService
    {
        Task<Product> GetProductAsync(Guid productId);
       
        Task<List<Product>> GetProductsByCategoryAsync(Guid categoryId);

        Task<List<Product>> SearchProductsAsync(string name);

        Task<List<Product>> GetAllProductsAsync();

        Task<Guid> AddProductAsync(Product product);

        Task UpdateProductAsync(Product product);

        Task DeleteProductAsync(Product product);
    }
}
