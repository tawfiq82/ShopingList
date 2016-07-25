using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopingList.Data.Sql.Repositories
{
    using System.Data.Entity;
    using SqlEntities = Entities;
    using Common.Contracts.DataContracts;

    public class CategoryRepository
    {
        readonly SqlEntities.ShopingListEntities _entities = new SqlEntities.ShopingListEntities();

        public static readonly Task CompletedTask = Task.FromResult(true);

        public Task<Category> GetCategoryAsync(Guid categoryId)
        {
            if (categoryId == Guid.Empty)
                throw new ArgumentNullException(nameof(categoryId));

            SqlEntities.Category categoryEntity = _entities.Categories.FirstOrDefault(x => x.CategoryID == categoryId);

            Category category = categoryEntity?.ToDataContractObject();
            return Task.FromResult(category);
        }

        public Task<List<Category>> GetAllCategoriesAsync()
        {
            List<Category> categories = _entities.Categories.ToDataContractObject();
            return Task.FromResult(categories);
        }

        public Task<Guid> AddCategoryAsync(Category category)
        {
            if (category == null)
                throw new ArgumentNullException(nameof(category));

            SqlEntities.Category categoryEntity = new SqlEntities.Category()
            {
                CategoryID = Guid.NewGuid(),
                Name = category.Name
            };
            try
            {
                _entities.Categories.Add(categoryEntity);
                _entities.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"Could not add the category. {ex.Message}");
            }
            return Task.FromResult(categoryEntity.CategoryID);
        }

        public Task UpdateCategoryAsync(Category category)
        {
            if (category == null)
                throw new ArgumentNullException(nameof(category));

            SqlEntities.Category categoryEntity =
                _entities.Categories.First(x => x.CategoryID == category.CategoryId);

            categoryEntity.Name = category.Name;

            try
            {
                _entities.Entry(categoryEntity).State = EntityState.Modified;
                _entities.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"Could not update the category. {ex.Message}");
            }
            return CompletedTask;
        }

        public Task DeleteCategoryAsync(Category category)
        {
            if (category == null)
                throw new ArgumentNullException(nameof(category));

            SqlEntities.Category categoryEntity =
                _entities.Categories.First(x => x.CategoryID == category.CategoryId);

            try
            {
                _entities.Categories.Remove(categoryEntity);
                _entities.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"Could not delete the category. {ex.Message}");
            }
            return CompletedTask;
        }
    }
}
