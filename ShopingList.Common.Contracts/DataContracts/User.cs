using System;

namespace ShopingList.Common.Contracts.DataContracts
{
    using Enums;

    public class User
    {
        public Guid UserId { get; set; }

        public string Name { get; set; }

        public UserType Type { get; set; }
    }
}
