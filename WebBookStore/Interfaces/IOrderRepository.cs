using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using WebBookStore.Models;

namespace WebBookStore.Interfaces
{
    public interface IOrderRepository
    {
        public ICollection<Order> GetOrders();
        public ICollection<Order> GetOrderByStatus(string status);
        public Order GetOrder(int id);
        bool CreateOrder(List<int> cartItemIds, Order order);
        bool UpdateOrder(int orderId, string status, int manageId);
        bool Save();
        bool OrderExists(int id);
    }

    }