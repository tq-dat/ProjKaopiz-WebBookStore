using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebBookStore.Models;

public class Cartitem
{
    public int Id { get; set; }
    public int QuantityOfProduct { get; set; }
    [MaxLength(10)]
    public string Status { get; set; }
    public int? OrderId { get; set; }
    public int ProductId { get; set; }
    public int UserId { get; set; }

    public Order? Order { get; set; }

    public Product Product { get; set; }

    public User User { get; set; }
}
