using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopingList.Services
{
    using Common.Contracts.DataContracts;
    using Common.Contracts.ServiceContracts;
    using Data.Sql.Repositories;

    public class ShoppingItemService : IShoppingItemService
    {
        private readonly ShoppingItemRepository _shoppingItemRepository;

        public ShoppingItemService()
        {
            _shoppingItemRepository = new ShoppingItemRepository();
        }

        public async Task<List<ShopingItem>> GetShoppingItemsAsync(User user)
        {
            return await _shoppingItemRepository.GetShoppingListAsync(user);
        }

        public async Task<Guid> AddShoppingItemAsync(User user, Product product)
        {
            return await _shoppingItemRepository.AddShoppingItemAsync(user, product);
        }

        public async Task RemoveShoppingItemAsync(Guid shoppingItemId)
        {
            await _shoppingItemRepository.RemoveShoppingItemAsync(shoppingItemId);
        }

        public async Task ClearAllShoppingItemsAsync(User user)
        {
            await _shoppingItemRepository.ClearAllShoppingItemsAsync(user);
        }

        public async Task CheckOutShoppingItemsAsync(IList<ShopingItem> shoppingItems)
        {
            await _shoppingItemRepository.CheckOutShoppingItemsAsync(shoppingItems);
        }
    }
}
