using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRN231.ASS01.Repository.Models
{
    public class Publisher
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PubId { get; set; }

        public string? PublisherName { get; set; }

        public string? City { get; set; }

        public string? State { get; set; }

        public string? Country { get; set; }
        public List<Book>? Books { get; set; }
        public List<User>? Users { get; set; }

    }
}
