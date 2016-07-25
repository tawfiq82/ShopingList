using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopingList.Data.Sql.Repositories
{
    using System.Data.Entity;
    using SqlEntities = Entities;
    using Common.Contracts.DataContracts;

    public class ProductRepository
    {
        readonly SqlEntities.ShopingListEntities _entities = new SqlEntities.ShopingListEntities();

        public static readonly Task CompletedTask = Task.FromResult(true);

        public Task<Product> GetProductAsync(Guid productId)
        {
            if (productId == Guid.Empty)
                throw new ArgumentNullException(nameof(productId));

            SqlEntities.Product productEntity = _entities.Products.FirstOrDefault(x => x.ProductID == productId);

            Product product = productEntity?.ToDataContractObject();
            return Task.FromResult(product);

        }

        public Task<List<Product>> GetProductsByCategoryAsync(Guid categoryId)
        {
            if (categoryId == Guid.Empty)
                throw new ArgumentNullException(nameof(categoryId));

            List<Product> products = _entities.Products.Where(x => x.CategoryID == categoryId)
                .ToDataContractObject();
            return Task.FromResult(products);
        }

        public Task<List<Product>> SearchProductsAsync(string name)
        {
            var products = new List<Product>();
            if (string.IsNullOrEmpty(name)) return Task.FromResult(products);

            products = _entities.Products.Where(x => x.Name.StartsWith(name)).ToDataContractObject();
            return Task.FromResult(products);
        }

        public Task<List<Product>> GetAllProductsAsync()
        {
            var products = _entities.Products.ToDataContractObject();
            return Task.FromResult(products);
        }

        public Task<Guid> AddProductAsync(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            var productId = Guid.NewGuid();

            SqlEntities.Product productEntity = new SqlEntities.Product()
            {
                ProductID = productId,
                Name = product.Name,
                ImgUrl = product.ImageUrl,
                CategoryID = product.Category.CategoryId
            };

            try
            {
                _entities.Products.Add(productEntity);
                _entities.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"Could not add the product. {ex.Message}");
            }
            return Task.FromResult(productEntity.ProductID);
        }

        public Task UpdateProductAsync(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            SqlEntities.Product productEntity = _entities.Products.First(x => x.ProductID == product.ProductId);

            productEntity.Name = product.Name;
            productEntity.ImgUrl = productEntity.ImgUrl;
            productEntity.CategoryID = product.Category.CategoryId;

            try
            {
                _entities.Entry(productEntity).State = EntityState.Modified;
                _entities.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"Could not update the product. {ex.Message}");
            }
            return CompletedTask;
        }

        public Task DeleteProductAsync(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            SqlEntities.Product productEntity =
                _entities.Products.First(x => x.ProductID == product.ProductId);

            try
            {
                _entities.Products.Remove(productEntity);
                _entities.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"Could not delete the product. {ex.Message}");
            }
            return CompletedTask;
        }
    }
}
