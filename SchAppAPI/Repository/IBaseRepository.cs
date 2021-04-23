using SchAppAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SchAppAPI.Repository
{
    public interface IBaseRepository<TEntity> where TEntity: BaseEntity
    {
        Task Add(TEntity entity);

        void Delete(TEntity entity);

        Task Delete(Guid Id);

        Task<IEnumerable<TEntity>> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");

        Task<IEnumerable<TEntity>> GetAll();

        Task<TEntity> GetById(Guid Id);

        Task<IEnumerable<TEntity>> GetWithRawSql(string query,
        params object[] parameters);

        void Update(TEntity entity);

        Task SaveChangesAsync();
    }
}
