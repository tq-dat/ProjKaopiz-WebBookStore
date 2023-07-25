using WebBookStore.Models;

namespace WebBookStore.Interfaces
{
    public interface ICartitemRepository
    {
        public ICollection<Cartitem> GetCartitems();
        public ICollection<Cartitem> GetCartitemByOrderId(int orderId);
        bool UpdateCartitem(Cartitem cartitem,int id);
        bool DeleteCartitem(int id);
        bool CreateCartitem(int productId, int userId, Cartitem cartitem);
        bool CartitemExists(int id);
        bool Save();
    }
}
