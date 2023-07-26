using Microsoft.EntityFrameworkCore;
using WebBookStore.Data;
using WebBookStore.Interfaces;
using WebBookStore.Models;
using ZstdSharp.Unsafe;

namespace WebBookStore.Repository
{
    public class CartItemRepository : ICartItemRepository
    {
        private readonly DataContext _context;

        public CartItemRepository(DataContext context) 
        {
            _context = context;
        }

        public bool CartItemExists(int id)
        {
            return _context.CartItems.Any(p => p.Id == id);
        }

        public bool CreateCartItem(int productId, int userId, CartItem cartItem)
        {
            var product = _context.Products.Where(p => p.Id == productId).FirstOrDefault();
            var user = _context.Users.Where(p => p.Id == userId).FirstOrDefault();
            
            cartItem.Product = product;
            cartItem.User = user;
     
            _context.Add(cartItem);
            return Save();
        }

        public bool DeleteCartItem(int id)
        {
            var deleteCartItem = _context.CartItems.Where(p => p.Id == id).FirstOrDefault();
            _context.Remove(deleteCartItem);
            return Save();
        }

        public ICollection<CartItem> GetCartItemByOrderId(int orderId)
        {
            return _context.CartItems.Where(p => p.OrderId == orderId).ToList();
        }

        public ICollection<CartItem> GetCartItems()
        {
            return _context.CartItems.ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateCartItem(CartItem cartItem, int id)
        {
            var cartUpdate = _context.CartItems.Where(p => p.Id  == id).FirstOrDefault();
            cartUpdate.QuantityOfProduct = cartItem.QuantityOfProduct;
            cartUpdate.Status = cartItem.Status;
            _context.Update(cartUpdate);
            return Save();
        }
    }
}
