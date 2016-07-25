using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopingList.Services
{
    using Common.Contracts.DataContracts;
    using Common.Contracts.ServiceContracts;
    using Data.Sql.Repositories;

    public class UserService : IUserService
    {
        private readonly UserRepository _userRepository;

        public UserService()
        {
            _userRepository= new UserRepository();
        }

        public async Task<User> GetUserAsync(Guid userId)
        {
            return await _userRepository.GetUserAsnyc(userId);
        }

        public async Task<User> GetUserAsync(string userName)
        {
            return await _userRepository.GetUserAsnyc(userName);
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllUsersAsync();
        }

        public async Task<Guid> AddUserAsync(User user)
        {
            return await _userRepository.AddUserAsync(user);
        }

        public async Task UpdateUserAsync(User user)
        {
            await _userRepository.UpdateUserAsync(user);
        }

        public async Task DeleteUserAsync(User user)
        {
            await _userRepository.DeleteUserAsync(user);
        }
    }
}
