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
            //    var productCategorys = new List<ProductCategory>()
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
            //                Cartitems = new List<Cartitem>()
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
            //                Cartitems = new List<Cartitem>()
            //            }
            //        }
            //    };
            //    dataContext.ProductCategories.AddRange(productCategorys);
            //    dataContext.SaveChanges();
            //}
            if (!dataContext.Cartitems.Any())
            {
                var cartitems = new List<Cartitem>()
                {
                    new Cartitem()
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
                dataContext.Cartitems.AddRange(cartitems);
                dataContext.SaveChanges();
            }
        }
    }
}
