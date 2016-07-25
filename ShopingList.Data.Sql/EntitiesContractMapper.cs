using System;
using System.Collections.Generic;
using System.Linq;

namespace ShopingList.Data.Sql
{
    using Common.Contracts.DataContracts;

    using Common.Contracts.Enums;

    using SqlEntities = Entities;

    public static class EntitiesContractMapper
    {
        public static Category ToDataContractObject(this SqlEntities.Category category)
        {
            if (category == null)
                throw new ArgumentNullException(nameof(category));

            return new Category()
            {
                CategoryId = category.CategoryID,
                Name = category.Name
            };
        }

        public static List<Category> ToDataContractObject(this IEnumerable<SqlEntities.Category> categories)
        {
            if (categories == null)
                throw new ArgumentNullException(nameof(categories));

            return categories.Select(category => category.ToDataContractObject()).ToList();
        }

        public static Product ToDataContractObject(this SqlEntities.Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            var productObj = new Product()
            {
                ProductId = product.ProductID,
                Name = product.Name,
                ImageUrl = product.ImgUrl,
                Category = product.Category.ToDataContractObject()
            };

            return productObj;
        }

        public static List<Product> ToDataContractObject(this IEnumerable<SqlEntities.Product> products)
        {
            if (products == null)
                throw new ArgumentNullException(nameof(products));

            return products.Select(product => product.ToDataContractObject()).ToList();
        }

        public static User ToDataContractObject(this SqlEntities.User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var userObj = new User {
                UserId = user.UserID,
                Name = user.Name
            };

            switch (user.Type)
            {
                case 1:
                    userObj.Type = UserType.Admin;
                    break;
                case 2:
                    userObj.Type = UserType.Normal;
                    break;
            }

            return userObj;
        }

        public static List<User> ToDataContractObject(this IEnumerable<SqlEntities.User> users)
        {
            if (users == null)
                throw new ArgumentNullException(nameof(users));

            return users.Select(user => user.ToDataContractObject()).ToList();
        }

        public static ShopingItem ToDataContractObject(this SqlEntities.ShopingList shoppingItem)
        {
            if (shoppingItem == null)
                throw new ArgumentNullException(nameof(shoppingItem));

            var shoppingItemObj = new ShopingItem {
                ShopingItemId = shoppingItem.ShoppingItemId,
                Product = shoppingItem.Product.ToDataContractObject(),
                User = shoppingItem.User.ToDataContractObject()
            };

            return shoppingItemObj;
        }

        public static List<ShopingItem> ToDataContractObject(this IEnumerable<SqlEntities.ShopingList> shoppingItems)
        {
            if (shoppingItems == null)
                throw new ArgumentNullException(nameof(shoppingItems));

            return shoppingItems.Select(shoppingItem => shoppingItem.ToDataContractObject()).ToList();
        }
    }
}
