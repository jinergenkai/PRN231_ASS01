using System.Linq.Expressions;

namespace PRN231.ASS01.Repository.Repository
{
    public interface IRepository<TEntity>
    {
        Task<IEnumerable<TEntity>> FindAllAsync(params Expression<Func<TEntity, object>>[] includes);
        Task<TEntity?> FindByIdAsync(Expression<Func<TEntity, bool>> predicate,
        params Expression<Func<TEntity, object>>[] includes);
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task DeleteAsync(int id);
    }
}
