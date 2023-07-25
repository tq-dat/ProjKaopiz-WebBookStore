using System.ComponentModel.DataAnnotations;

namespace WebBookStore.Dto
{
    public class ProductDto
    {
        public int Id { get; set; }
        [MaxLength(255)]
        public string Name { get; set; }
        [MaxLength(500)]
        public string Description { get; set; } = null!;
        [MaxLength(255)]
        public string Author { get; set; } = null!;
        public double Price { get; set; }
    }
}
