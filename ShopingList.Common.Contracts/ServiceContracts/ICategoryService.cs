using System;
using System.Collections.Generic;

namespace ShopingList.Common.Contracts.ServiceContracts
{
    using System.Threading.Tasks;

    using DataContracts;

    public interface ICategoryService
    {
        Task<Category> GetCategoryAsync(Guid categoryId);

        Task<List<Category>> GetAllCategoriesAsync();

        Task<Guid> AddCategoryAsync(Category category);

        Task UpdateCategoryAsync(Category category);

        Task DeleteCategoryAsync(Category category);
    }
}
