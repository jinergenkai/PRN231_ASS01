using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRN231.ASS01.Repository.Models
{
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookId { get; set; }
        public string? Title { get; set; }
        public string? Type { get; set; }
        [Required]
        public int PubId { get; set; }
        public decimal Price { get; set; }
        public decimal Advance { get; set; }
        public decimal Royalty { get; set; }
        public decimal YtdSales { get; set; }
        public string? Notes { get; set; }
        public DateTime PublishedDate { get; set; }
        public Publisher? Publisher { get; set; }
        public List<BookAuthor>? BookAuthors { get; internal set; }
    }
}
