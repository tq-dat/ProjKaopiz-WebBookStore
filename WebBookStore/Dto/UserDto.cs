using System.ComponentModel.DataAnnotations;

namespace WebBookStore.Dto
{
    public class UserDto
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
    }
}
