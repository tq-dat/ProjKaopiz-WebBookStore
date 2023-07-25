using Microsoft.EntityFrameworkCore;
using WebBookStore.Data;
using WebBookStore.Interfaces;
using WebBookStore.Models;
using ZstdSharp.Unsafe;

namespace WebBookStore.Repository
{
    public class CartitemRepository : ICartitemRepository
    {
        private readonly DataContext _context;

        public CartitemRepository(DataContext context) 
        {
            _context = context;
        }

        public bool CartitemExists(int id)
        {
            return _context.Cartitems.Any(p => p.Id == id);
        }

        public bool CreateCartitem(int productId, int userId, Cartitem cartitem)
        {
            var product = _context.Products.Where(p => p.Id == productId).FirstOrDefault();
            var user = _context.Users.Where(p => p.Id == userId).FirstOrDefault();
            
            cartitem.Product = product;
            cartitem.User = user;
     
            _context.Add(cartitem);
            return Save();
        }

        public bool DeleteCartitem(int id)
        {
            var deleteCartitem = _context.Cartitems.Where(p => p.Id == id).FirstOrDefault();
            _context.Remove(deleteCartitem);
            return Save();
        }

        public ICollection<Cartitem> GetCartitemByOrderId(int orderId)
        {
            return _context.Cartitems.Where(p => p.OrderId == orderId).ToList();
        }

        public ICollection<Cartitem> GetCartitems()
        {
            return _context.Cartitems.ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateCartitem(Cartitem cartitem, int id)
        {
            var cartUpdate = _context.Cartitems.Where(p => p.Id  == id).FirstOrDefault();
            cartUpdate.QuantityOfProduct = cartitem.QuantityOfProduct;
            cartUpdate.Status = cartitem.Status;
            _context.Update(cartUpdate);
            return Save();
        }
    }
}
