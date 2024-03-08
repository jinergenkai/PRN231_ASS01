using Microsoft.EntityFrameworkCore;

namespace PRN231.ASS01.Repository.Models
{
    public class BookStoreDBContext : DbContext
    {
        public BookStoreDBContext()
        {
        }
        public BookStoreDBContext(DbContextOptions<BookStoreDBContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().HasOne(b => b.Publisher).WithMany(p => p.Books).HasForeignKey(b => b.PubId);
            modelBuilder.Entity<User>().HasOne(u => u.Publisher).WithMany(p => p.Users).HasForeignKey(u => u.PubId);
            modelBuilder.Entity<User>().HasOne(u => u.Role).WithMany(r => r.Users).HasForeignKey(u => u.RoleId);
            modelBuilder.Entity<BookAuthor>().HasKey(b => new { b.BookId, b.AuthorId });
            modelBuilder.Entity<BookAuthor>().HasOne(u => u.Author).WithMany(a => a.BookAuthors).HasForeignKey(u => u.AuthorId);
            modelBuilder.Entity<BookAuthor>().HasOne(u => u.Book).WithMany(b => b.BookAuthors).HasForeignKey(u => u.BookId);
        }

        //* xin loi thay, em chua hieu lam ve cai nay, em se hoi thay sau
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
                => optionsBuilder.UseSqlServer("Server=(local);uid=sa;pwd=12345;database=BookStoreDB;encrypt=false");
    }
}
