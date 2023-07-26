using WebBookStore.Dto;
using WebBookStore.Models;

namespace WebBookStore.Interfaces
{
    public interface IUserRepository
    {
        ICollection<User> GetUsers();
        ICollection<User> GetUsersByRole(string role);
        User GetUser(int userId);
        ICollection<User> GetUsersByName(string name);
        ICollection<CartItem> GetCartItemByUserId (int userId);
        ICollection<Order> GetOrdersByUserId(int userId);
        bool UserExists (UserLogin userLogin);
        public bool UserExists(int userId);
        bool CreateUser(User user);
        bool UpdateUser(User user);
        bool DeleteUser(int id);
        bool Save();
    }
}
