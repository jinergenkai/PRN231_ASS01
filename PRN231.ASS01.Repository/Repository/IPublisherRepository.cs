using PRN231.ASS01.Repository.Models;

namespace PRN231.ASS01.Repository.Repository
{
    public interface IPublisherRepository
    {
        Task<IEnumerable<Publisher>> FindAllAsync();
        Task<Publisher?> FindByIdAsync(int id);
        Task<Publisher> AddAsync(Publisher publisher);
        Task<Publisher> UpdateAsync(Publisher publisher);
        Task DeleteAsync(int id);
    }
}
