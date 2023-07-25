using WebBookStore.Models;

namespace WebBookStore.Interfaces
{
    public interface ICategoryRepository
    {
        ICollection<Category> GetCategories();
        Category GetCategory(int id);
        ICollection<Product> GetProductsByCategory(int categoryId);
        bool UpdateCategory(int categoryId, string Name);
        bool DeleteCategory(int id);
        bool CategoryExists(int id);
        bool CreateCategory(Category category);
        bool Save();
    }
}
