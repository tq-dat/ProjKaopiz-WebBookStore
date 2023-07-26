using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebBookStore.Models;

public class User
{
    public int Id { get; set; }
    [MaxLength(255)]
    public string FullName { get; set; }
    [MaxLength(255)]
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    [MaxLength(255)]
    public string Address { get; set; }
    [MaxLength(10)]
    public string Role { get; set; }
    public ICollection<CartItem> CartItems { get; set; }
    public ICollection<Order> Orders { get; set; }
}
