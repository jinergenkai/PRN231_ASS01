using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRN231.ASS01.Repository.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        [Required]
        public string? EmailAddress { get; set; }
        [Required]
        public string? Password { get; set; }

        public string? Source { get; set; }
        [Required]
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        [Required]
        public int RoleId { get; set; }
        [Required]
        public int PubId { get; set; }
        public DateTime HireDate { get; set; }
        public Publisher? Publisher { get; set; }
        public Role? Role { get; set; }
    }
}
