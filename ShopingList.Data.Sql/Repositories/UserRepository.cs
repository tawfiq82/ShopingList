using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopingList.Data.Sql.Repositories
{
    using System.Data.Entity;
    using SqlEntities = Entities;
    using Common.Contracts.DataContracts;

    public class UserRepository
    {
        readonly SqlEntities.ShopingListEntities _entities = new SqlEntities.ShopingListEntities();

        public static readonly Task CompletedTask = Task.FromResult(true);

        public Task<User> GetUserAsnyc(Guid userId)
        {
            if (userId == Guid.Empty)
                throw new ArgumentNullException(nameof(userId));

            SqlEntities.User userEntity = _entities.Users.FirstOrDefault(x => x.UserID == userId);

            User user = userEntity?.ToDataContractObject();
            return Task.FromResult(user);
        }

        public Task<User> GetUserAsnyc(string userName)
        {
            if (string.IsNullOrEmpty(userName))
                throw new ArgumentNullException(nameof(userName));

            SqlEntities.User userEntity = _entities.Users.FirstOrDefault(x => x.Name.ToUpper() == userName.ToUpper());

            User user = userEntity?.ToDataContractObject();
            return Task.FromResult(user);
        }

        public Task<List<User>> GetAllUsersAsync()
        {
            List<User> users = _entities.Users.ToDataContractObject();
            return Task.FromResult(users);
        }

        public Task<Guid> AddUserAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            SqlEntities.User userEntity = new SqlEntities.User()
            {
                UserID = Guid.NewGuid(),
                Name = user.Name,
                Type = (int)user.Type
            };

            try
            {
                _entities.Users.Add(userEntity);
                _entities.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"Could not add the user. {ex.Message}");
            }
            return Task.FromResult(userEntity.UserID);
        }

        public Task UpdateUserAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            SqlEntities.User userEntity =
                _entities.Users.First(x => x.UserID == user.UserId);

            userEntity.Name = user.Name;

            try
            {
                _entities.Entry(userEntity).State = EntityState.Modified;
                _entities.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"Could not update the user. {ex.Message}");
            }
            return CompletedTask;
        }

        public Task DeleteUserAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            SqlEntities.User userEntity =
                _entities.Users.First(x => x.UserID == user.UserId);

            try
            {
                _entities.Users.Remove(userEntity);
                _entities.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"Could not delete the user. {ex.Message}");
            }
            return CompletedTask;
        }
    }
}
