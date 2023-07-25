using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebBookStore.Models;

public class Product
{
    public int Id { get; set; }
    [MaxLength(255)]
    public string Name { get; set; }
    [MaxLength(500)]
    public string Description { get; set; } = null!;
    [MaxLength(255)]
    public string Author { get; set; } = null!;
    public double Price { get; set; }
    public ICollection<ProductCategory> ProductCategories { get; set; }
    public ICollection<Cartitem> Cartitems { get; set; }
}
