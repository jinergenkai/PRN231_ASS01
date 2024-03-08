using Microsoft.EntityFrameworkCore;
using PRN231.ASS01.Repository.Models;

namespace PRN231.ASS01.Repository.Repository
{
    public class BookAuthorRepository : IBookAuthorRepository
    {
        private readonly BookStoreDBContext _dbContext;
        public BookAuthorRepository(BookStoreDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<BookAuthor> AddAsync(BookAuthor bookAuthor)
        {
            _dbContext.BookAuthors.Add(bookAuthor);
            await _dbContext.SaveChangesAsync();
            return bookAuthor;
        }

        public async Task DeleteAsync(int authorId, int bookId)
        {
            var bookAuthor = await _dbContext.BookAuthors.FirstOrDefaultAsync(bA => bA.AuthorId == authorId && bA.BookId == bookId);
            if (bookAuthor != null)
            {
                _dbContext.BookAuthors.Remove(bookAuthor);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<BookAuthor>> FindAllAsync()
        {
            return await _dbContext.BookAuthors.Include(b => b.Book).Include(b => b.Author).ToListAsync();
        }

        public async Task<BookAuthor?> FindByIdAsync(int authorId, int bookId)
        {
            return await _dbContext.BookAuthors.Include(b => b.Book).Include(b => b.Author).FirstOrDefaultAsync(bA => bA.AuthorId == authorId && bA.BookId == bookId);
        }

        public async Task<BookAuthor> UpdateAsync(BookAuthor bookAuthor)
        {
            _dbContext.BookAuthors.Entry(bookAuthor).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return bookAuthor;
        }
    }
}
