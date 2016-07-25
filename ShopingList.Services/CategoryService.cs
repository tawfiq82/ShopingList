using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopingList.Services
{
    using Common.Contracts.DataContracts;
    using Common.Contracts.ServiceContracts;
    using Data.Sql.Repositories;

    public class CategoryService : ICategoryService
    {
        private readonly CategoryRepository _categoryRepository;

        public CategoryService()
        {
            _categoryRepository = new CategoryRepository();
        }

        public async Task<Category> GetCategoryAsync(Guid categoryId)
        {
            return await _categoryRepository.GetCategoryAsync(categoryId);
        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            return await _categoryRepository.GetAllCategoriesAsync();
        }

        public async Task<Guid> AddCategoryAsync(Category category)
        {
           return await _categoryRepository.AddCategoryAsync(category);
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            await _categoryRepository.UpdateCategoryAsync(category);
        }

        public async Task DeleteCategoryAsync(Category category)
        {
            await _categoryRepository.DeleteCategoryAsync(category);
        }
    }
}
