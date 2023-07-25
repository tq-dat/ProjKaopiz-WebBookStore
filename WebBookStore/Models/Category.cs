using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebBookStore.Models;

public class Category
{
    public int Id { get; set; }
    [MaxLength(100)]
    public string Name { get; set; }
    public ICollection<ProductCategory> ProductCategories;
}
