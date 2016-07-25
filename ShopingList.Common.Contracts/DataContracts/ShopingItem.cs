using System;

namespace ShopingList.Common.Contracts.DataContracts
{
    public class ShopingItem
    {
        public Guid ShopingItemId { get; set; }

        public User User { get; set; }

        public Product Product { get; set; }
    }
}
