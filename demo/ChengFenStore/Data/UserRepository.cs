using ChengFenStore.Models;
using System.Collections.Generic;
using System.Linq;

namespace ChengFenStore.Data
{
    public interface IUserRepository
    {
        void AddUser(User user);
        User GetUserByPhoneNumber(string phoneNumber);
    }

    public class UserRepository : IUserRepository
    {
        private readonly List<User> _users = new List<User>();

        public void AddUser(User user)
        {
            _users.Add(user);
        }

        public User GetUserByPhoneNumber(string phoneNumber)
        {
            return _users.FirstOrDefault(u => u.PhoneNumber == phoneNumber);
        }
    }
}
