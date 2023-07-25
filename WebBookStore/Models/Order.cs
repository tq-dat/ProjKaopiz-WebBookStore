using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebBookStore.Models;

public class Order
{
    public int Id { get; set; }
    public DateTime DateOrder { get; set; }
    public int? UserAdminId { get; set; }
    [MaxLength(10)]
    public string Status { get; set; }
    public ICollection<Cartitem> Cartitems { get; set; }
}
