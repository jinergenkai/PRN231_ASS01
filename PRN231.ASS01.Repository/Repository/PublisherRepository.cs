using Microsoft.EntityFrameworkCore;
using PRN231.ASS01.Repository.Models;

namespace PRN231.ASS01.Repository.Repository
{
    public class PublisherRepository : IPublisherRepository
    {
        private readonly BookStoreDBContext _dbContext;
        public PublisherRepository(BookStoreDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Publisher> AddAsync(Publisher publisher)
        {
            _dbContext.Publishers.Add(publisher);
            await _dbContext.SaveChangesAsync();
            return publisher;
        }

        public async Task DeleteAsync(int id)
        {
            var publisher = await _dbContext.Publishers.FindAsync(id);
            if (publisher != null)
            {
                _dbContext.Publishers.Remove(publisher);
                await _dbContext.SaveChangesAsync();
            }

        }

        public async Task<IEnumerable<Publisher>> FindAllAsync()
        {
            return await _dbContext.Publishers.ToListAsync();
        }

        public async Task<Publisher?> FindByIdAsync(int id)
        {
            return await _dbContext.Publishers.FindAsync(id);
        }

        public async Task<Publisher> UpdateAsync(Publisher publisher)
        {
            _dbContext.Entry(publisher).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return publisher;
        }
    }
}
