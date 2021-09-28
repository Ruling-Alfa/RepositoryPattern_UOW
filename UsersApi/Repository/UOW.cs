using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UsersApi.Repository
{
    public class Uow<TEntity> : IUow<TEntity> where TEntity : class
    {
        private readonly DbContext dbContext;
        private readonly IRepository<TEntity> _repo;
        public Uow(IRepository<TEntity> repo)
        {
            dbContext = new UserContext();
            _repo = repo;
        }

        public IRepository<TEntity> GetRepository()
        {
            return _repo;
        }

        public Task CommitAsync()
        {
            if (dbContext is null)
            {
                throw new ArgumentNullException($"{nameof(dbContext)} Context is null");
            }
            return dbContext.SaveChangesAsync();
        }
    }
}
