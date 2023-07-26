using WebBookStore.Data;
using WebBookStore.Interfaces;
using WebBookStore.Models;

namespace WebBookStore.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DataContext _context;

        public OrderRepository(DataContext context) {
            _context = context;
        }

        public bool CreateOrder(List<int> cartItemIds, Order order)
        {
            foreach (int id in cartItemIds)
            {
                var cartItem = _context.CartItems.Where( p => p.Id == id).FirstOrDefault();
                cartItem.Order = order;
                cartItem.Status = "Paid";
                _context.Update(cartItem);
            }
            _context.Add(order);
            return Save();
        }

        public Order GetOrder(int id)
        {
            return _context.Orders.Where(p => p.Id == id).FirstOrDefault();
        }

        public ICollection<Order> GetOrderByStatus(string status)
        {
            var orders = _context.Orders.Where(p => p.Status.Contains(status)).ToList();
            return orders;   
        }

        public ICollection<Order> GetOrders()
        {
            return _context.Orders.ToList();
        }

        public bool OrderExists(int id)
        {
            return _context.Orders.Any(p => p.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateOrder(int orderId,string status, int manageId)
        {
            var orderUpdate = _context.Orders.Where(p => p.Id == orderId).FirstOrDefault();
            if (orderUpdate == null)
                return false;
            var admin = _context.Users.Where(p => p.Id == manageId && (p.Role == "Manage" || p.Role == "Admin")).FirstOrDefault();
            if (admin == null)
                return false;
            orderUpdate.Status = status;
            orderUpdate.UserAdminId = manageId;
            _context.Update(orderUpdate);
            return Save();
        }
    }
}
