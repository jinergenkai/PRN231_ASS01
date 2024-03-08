using System.ComponentModel.DataAnnotations;

namespace PRN231.ASS01.BookStoreAPI.Models
{
    public class LoginRequest
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
