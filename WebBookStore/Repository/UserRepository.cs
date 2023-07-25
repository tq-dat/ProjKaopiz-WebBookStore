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
            var deleteCartitemIds = _context.Cartitems.Where(P => P.UserId == id && P.Status == "UnPaid").Select(p =>p.Id).ToList();
            foreach (var cartitemId in deleteCartitemIds)
            {
                var cartitem = _context.Cartitems.Where(p => p.Id == cartitemId).FirstOrDefault();
                _context.Remove(cartitem);
            }
            deleteUser.Role = "UserDeleted";
            _context.Update(deleteUser);
            return Save();
        }

        public ICollection<Cartitem> GetCartitemByUserId(int userId)
        {
            return _context.Cartitems.Where(c => c.UserId == userId && c.Status == "UnPaid").ToList();
        }

        public ICollection<Order> GetOrdersByUserId(int userId)
        {
            return _context.Cartitems.Where(c => c.UserId == userId && c.Status == "Paid").Select(c => c.Order).ToList();
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
