using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopingList.Common.Contracts.ServiceContracts
{
    using DataContracts;

    public interface IShoppingItemService
    {
        Task<List<ShopingItem>> GetShoppingItemsAsync(User user);

        Task<Guid> AddShoppingItemAsync(User user, Product product);

        Task RemoveShoppingItemAsync(Guid shoppingItemId);

        Task ClearAllShoppingItemsAsync(User user);

        Task CheckOutShoppingItemsAsync(IList<ShopingItem> shoppingItems);
    }
}
