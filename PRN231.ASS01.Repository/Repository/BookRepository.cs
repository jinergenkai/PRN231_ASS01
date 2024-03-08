using Microsoft.EntityFrameworkCore;
using PRN231.ASS01.Repository.Models;

namespace PRN231.ASS01.Repository.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly BookStoreDBContext _dbContext;
        public BookRepository(BookStoreDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Book> AddAsync(Book book)
        {
            _dbContext.Books.Add(book);
            await _dbContext.SaveChangesAsync();
            return book;
        }

        public async Task DeleteAsync(int id)
        {
            var book = await _dbContext.Books.FindAsync(id);
            if (book != null)
            {
                _dbContext.Books.Remove(book);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Book>> FindAllAsync()
        {
            return await _dbContext.Books.Include(b => b.Publisher).ToListAsync();
        }

        public async Task<Book?> FindByIdAsync(int id)
        {
            return await _dbContext.Books.Include(b => b.Publisher).FirstOrDefaultAsync(b => b.BookId == id);
        }

        public async Task<Book> UpdateAsync(Book book)
        {
            _dbContext.Books.Entry(book).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return book;
        }
    }
}
