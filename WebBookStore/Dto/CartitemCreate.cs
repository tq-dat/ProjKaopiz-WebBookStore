using System.ComponentModel.DataAnnotations;

namespace WebBookStore.Dto
{
    public class CartItemCreate
    {
        public int QuantityOfProduct { get; set; }
        [MaxLength(10)]
        public string Status { get; set; }
    }
}
