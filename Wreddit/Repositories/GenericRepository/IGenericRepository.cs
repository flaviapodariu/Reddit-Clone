using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wreddit.Repositories
{
    public interface IGenericRepository<TEntity>
    {
        //get data
        IQueryable<TEntity> GetAll();
        Task<TEntity> GetByIdAsync(int id);

        //create
        void Create(TEntity entity);
        void CreateRange(IEnumerable<TEntity> entities);

        //delete
        void Delete(TEntity entity);
        void DeleteRange(IEnumerable<TEntity> entities);

        //save
        Task<bool> SaveAsync();

    }
}
