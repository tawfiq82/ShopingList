using System;

namespace ShopingList.Common.Contracts.DataContracts
{
    using System.Collections.Generic;

    public class Category
    {
        public Guid CategoryId { get; set; }

        public string Name { get; set; }

        public IList<Product> ProductList { get; set; }
    }
}
