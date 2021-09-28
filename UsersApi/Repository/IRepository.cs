using System.Linq;
using System.Threading.Tasks;

namespace UsersApi.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> AddAsync(TEntity entity);
        IQueryable<TEntity> GetAll();
        Task<TEntity> RemoveAsync(TEntity entity);
        //Task<int> SaveChangesAsync();
        Task<TEntity> UpdateAsync(TEntity entity);
    }
}