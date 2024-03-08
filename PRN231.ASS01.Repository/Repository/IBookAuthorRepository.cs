using PRN231.ASS01.Repository.Models;

namespace PRN231.ASS01.Repository.Repository
{
    public interface IBookAuthorRepository
    {
        Task<IEnumerable<BookAuthor>> FindAllAsync();
        Task<BookAuthor?> FindByIdAsync(int authorId, int bookId);
        Task<BookAuthor> AddAsync(BookAuthor bookAuthor);
        Task<BookAuthor> UpdateAsync(BookAuthor bookAuthor);
        Task DeleteAsync(int authorId, int bookId);
    }
}
