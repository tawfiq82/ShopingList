using System;

namespace ShopingList.Common.Contracts.DataContracts
{
    using System.Collections.Generic;

    public class Product
    {
        public Guid ProductId { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public Category Category { get; set; }
    }
}
