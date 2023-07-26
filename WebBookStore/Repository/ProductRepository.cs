using WebBookStore.Data;
using WebBookStore.Interfaces;
using WebBookStore.Models;
using ZstdSharp.Unsafe;

namespace WebBookStore.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext _context;

        public ProductRepository(DataContext context) 
        {
            _context = context;
        }
        public Product GetProduct(int id)
        {
            return _context.Products.Where(p => p.Id == id).FirstOrDefault();
        }

        public ICollection<Product> GetProductsByName(string name)
        {
            return _context.Products.Where(p => p.Name.Contains(name)).ToList();
        }

        public ICollection<Product> GetProducts()
        {
            return _context.Products.OrderBy(p => p.Id).ToList();
        }

        public bool ProductExists(int prodId)
        {
            return _context.Products.Any(p => p.Id == prodId);
        }

        public bool CreateProduct(int categoryId, Product product)
        {
            var category = _context.Categories.Where(p => p.Id ==  categoryId).FirstOrDefault();
            var productCategory = new ProductCategory()
            {
                Product = product,
                Category = category
            };
            _context.Add(productCategory);
            _context.Add(product);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateProduct(int productId, Product product)
        {
            var productUpdate = _context.Products.Where(p => p.Id == productId).FirstOrDefault();
            productUpdate.Name = product.Name;
            productUpdate.Description = product.Description;
            productUpdate.Author = product.Author;
            productUpdate.Price = product.Price;
            _context.Update(productUpdate);
            return Save();
        }

        public bool DeleteProduct(int productId)
        {
            var deleteUpdate = _context.Products.Where(p => p.Id == productId).FirstOrDefault();
            var productcategories = _context.ProductCategories.Where(p => p.ProductId == productId).ToList();
            foreach (var pc in productcategories)
            {
                _context.Remove(pc);
            }
            var cartItems = _context.CartItems.Where(p => p.ProductId == productId && p.Status == "UnPaid").ToList();
            foreach (var cartItem in cartItems)
            {
                _context.Remove(cartItem);
            }
            _context.Remove(deleteUpdate);
            return Save();
        }
    }
}
