using WebBookStore.Data;
using WebBookStore.Models;

namespace WebBookStore
{
    public class Seed
    {
        private readonly DataContext dataContext;
        public Seed(DataContext context)
        {
            this.dataContext = context;
        }

        public void SeedDataContext()
        {
            //if(!dataContext.ProductCategories.Any())
            //{
            //    var productcategories = new List<ProductCategory>()
            //    {
            //        new ProductCategory()
            //        {
            //            Product = new Product()
            //            {
            //                Name = "Bạch dạ hành",
            //                Description = "Không có",
            //                Author = "Higashino Keigo",
            //                Price = 150000,
            //                ProductCategories = new List<ProductCategory>()
            //                {
            //                    new ProductCategory { Category = new Category() { Name = "Tiểu thuyết"}}
            //                },
            //                CartItems = new List<CartItem>()
            //            }
            //        },
            //        new ProductCategory()
            //        {
            //            Product = new Product()
            //            {
            //                Name = "Hành trình về phương đông",
            //                Description = "Không có",
            //                Author = "Baird T Spalding",
            //                Price = 150000,
            //                ProductCategories = new List<ProductCategory>()
            //                {
            //                    new ProductCategory { Category = new Category() { Name = "Sách Tôn giáo - Tâm linh"}}
            //                },
            //                CartItems = new List<CartItem>()
            //            }
            //        }
            //    };
            //    dataContext.ProductCategories.AddRange(productcategories);
            //    dataContext.SaveChanges();
            //}
            if (!dataContext.CartItems.Any())
            {
                var cartItems = new List<CartItem>()
                {
                    new CartItem()
                    {
                        QuantityOfProduct = 2,
                        Status = "UnPaid",
                        Order = new Order()
                        {
                            DateOrder = new DateTime(2023,7,24),
                            UserAdminId = null,
                            Status = "wait"

                        },
                        Product = new Product()
                        {
                            Name = "Bạch dạ hành",
                            Description = "Không có",
                            Author = "Higashino Keigo",
                            Price = 150000,
                            ProductCategories = new List<ProductCategory>()
                            {
                                new ProductCategory { Category = new Category() { Name = "Tiểu thuyết"}}
                            }
                        },
                        User = new User()
                        {
                            FullName = "Trinh Dat",
                            UserName = "dat123",
                            Password = "dat123",
                            Email = "dat123@gmail.com",
                            Address = "Cau Giay, Ha Noi",
                            Role = "User"
                        }
                    }
                };
                dataContext.CartItems.AddRange(cartItems);
                dataContext.SaveChanges();
            }
        }
    }
}
