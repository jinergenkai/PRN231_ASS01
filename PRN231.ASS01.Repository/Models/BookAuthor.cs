using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PRN231.ASS01.Repository.Models
{
    public class BookAuthor
    {
        [Key]
        [Column(Order = 1)]
        public int AuthorId { get; set; }
        [Key]
        [Column(Order = 2)]
        public int BookId { get; set; }
        public int AuthorOrder { get; set; }
        public double RoyalityPercentage { get; set; }
        [JsonIgnore]
        public Author? Author { get; set; }
        [JsonIgnore]
        public Book? Book { get; set; }
    }
}
