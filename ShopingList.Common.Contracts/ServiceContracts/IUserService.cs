namespace ShopingList.Common.Contracts.ServiceContracts
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DataContracts;

    public interface IUserService
    {
        Task<User> GetUserAsync(Guid userId);

        Task<User> GetUserAsync(string userName);

        Task<List<User>> GetAllUsersAsync();

        Task<Guid> AddUserAsync(User user);

        Task UpdateUserAsync(User user);

        Task DeleteUserAsync(User user);
    }
}
