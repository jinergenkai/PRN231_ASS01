using PRN231.ASS01.Repository.Models;

namespace PRN231.ASS01.Repository.Repository
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> FindAllAsync();
        Task<Book?> FindByIdAsync(int id);
        Task<Book> AddAsync(Book book);
        Task<Book> UpdateAsync(Book book);
        Task DeleteAsync(int id);
    }
}
