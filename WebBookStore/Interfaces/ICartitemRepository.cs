using WebBookStore.Models;

namespace WebBookStore.Interfaces
{
    public interface ICartItemRepository
    {
        public ICollection<CartItem> GetCartItems();
        public ICollection<CartItem> GetCartItemByOrderId(int orderId);
        bool UpdateCartItem(CartItem cartItem,int id);
        bool DeleteCartItem(int id);
        bool CreateCartItem(int productId, int userId, CartItem cartItem);
        bool CartItemExists(int id);
        bool Save();
    }
}
