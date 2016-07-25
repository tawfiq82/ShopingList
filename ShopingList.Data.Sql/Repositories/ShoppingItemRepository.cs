using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopingList.Data.Sql.Repositories
{
    using Common.Contracts.DataContracts;
    using SqlEntities = Entities;

    public class ShoppingItemRepository
    {
        readonly SqlEntities.ShopingListEntities _entities = new SqlEntities.ShopingListEntities();

        public static readonly Task CompletedTask = Task.FromResult(true);

        public Task<List<ShopingItem>> GetShoppingListAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            return Task.FromResult(_entities.ShopingLists.Where(x => x.UserID == user.UserId).ToDataContractObject());
        }

        public Task<Guid> AddShoppingItemAsync(User user, Product product)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            if (product == null)
                throw new ArgumentNullException(nameof(product));

            SqlEntities.ShopingList shoppingItemEntity = new SqlEntities.ShopingList
            {
                ShoppingItemId = Guid.NewGuid(),
                UserID = user.UserId,
                ProductID = product.ProductId
            };

            try
            {
                _entities.ShopingLists.Add(shoppingItemEntity);
                _entities.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"Could not add the shopping item. {ex.Message}");
            }
            return Task.FromResult(shoppingItemEntity.ShoppingItemId);
        }

        public Task RemoveShoppingItemAsync(Guid shoppingItemId)
        {
            if (shoppingItemId == Guid.Empty)
                throw new ArgumentNullException(nameof(shoppingItemId));

            SqlEntities.ShopingList shoppingItemEntity =
                _entities.ShopingLists.First(x => x.ShoppingItemId == shoppingItemId);

            try
            {
                _entities.ShopingLists.Remove(shoppingItemEntity);
                _entities.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"Could not remove the shopping item. {ex.Message}");
            }
            return CompletedTask;
        }

        public Task ClearAllShoppingItemsAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            try
            {
                _entities.ShopingLists.RemoveRange(_entities.ShopingLists.Where(x => x.UserID == user.UserId));
                _entities.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"Could not clear all the shopping items for user '{user.Name}'. {ex.Message}");
            }
            return CompletedTask;
        }

        public Task CheckOutShoppingItemsAsync(IList<ShopingItem> shoppingItems)
        {
            if (shoppingItems == null)
                throw new ArgumentNullException(nameof(shoppingItems));

            if (shoppingItems.Count == 0)
                return CompletedTask;

            var user = shoppingItems.First().User;
            var itemIds = shoppingItems.Select(x => x.ShopingItemId).ToArray();
            try
            {
                _entities.ShopingLists.RemoveRange(_entities.ShopingLists.Where(x => itemIds.Contains(x.ShoppingItemId)));
                _entities.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"Could not check out the shopping items for user '{user.Name}'. {ex.Message}");
            }
            return CompletedTask;
        }
    }
}
