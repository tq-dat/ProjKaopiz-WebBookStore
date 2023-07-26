using WebBookStore.Data;
using WebBookStore.Dto;
using WebBookStore.Interfaces;
using WebBookStore.Models;

namespace WebBookStore.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context) 
        {
            _context = context;
        }

        public bool CreateUser(User user)
        {
            _context.Add(user);
            return Save();
        }

        public bool DeleteUser(int id)
        {
            var deleteUser = _context.Users.Where(p => p.Id == id).FirstOrDefault();
            var deleteCartItemIds = _context.CartItems.Where(P => P.UserId == id && P.Status == "UnPaid").Select(p =>p.Id).ToList();
            foreach (var cartItemId in deleteCartItemIds)
            {
                var cartItem = _context.CartItems.Where(p => p.Id == cartItemId).FirstOrDefault();
                _context.Remove(cartItem);
            }
            deleteUser.Role = "UserDeleted";
            _context.Update(deleteUser);    
            return Save();
        }

        public ICollection<CartItem> GetCartItemByUserId(int userId)
        {
            return _context.CartItems.Where(c => c.UserId == userId && c.Status == "UnPaid").ToList();
        }

        public ICollection<Order> GetOrdersByUserId(int userId)
        {
            return _context.CartItems.Where(c => c.UserId == userId && c.Status == "Paid").Select(c => c.Order).ToList();
        }

        public User GetUser(int userId)
        {
            return _context.Users.Where(u => u.Id == userId).FirstOrDefault();
        }

        public ICollection<User> GetUsers()
        {
            return _context.Users.ToList();
        }

        public ICollection<User> GetUsersByName(string name)
        {
            return _context.Users.Where(p => p.UserName.Contains(name)).ToList();
        }

        public ICollection<User> GetUsersByRole(string role)
        {
            return _context.Users.Where(u => u.Role == role).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateUser(User user)
        {
            _context.Update(user);
            return Save();
        }

        public bool UserExists(int userId)
        {
            return _context.Users.Any(p => p.Id == userId);
        }

        public bool UserExists(UserLogin userLogin)
        {
            return _context.Users.Any(p => p.UserName == userLogin.UserName && p.Password == userLogin.Password && p.Role == userLogin.Role);
        }
    }
}
