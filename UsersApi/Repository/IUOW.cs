using System.Threading.Tasks;

namespace UsersApi.Repository
{
    public interface IUow<TEntity> where TEntity : class
    {
        Task CommitAsync();
        IRepository<TEntity> GetRepository();
    }
}