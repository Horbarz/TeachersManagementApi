using Microsoft.EntityFrameworkCore;
using SchAppAPI.Contexts;
using SchAppAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SchAppAPI.Repository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        private SchoolDbContext _paymentProcessorContext;
        private DbSet<TEntity> _dbSet;

        public BaseRepository(SchoolDbContext paymentProcessorContext)
        {
            _paymentProcessorContext = paymentProcessorContext;
            _dbSet = _paymentProcessorContext.Set<TEntity>();
        }

        public async Task Add(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException();
            await _dbSet.AddAsync(entity);
        }

        public void Delete(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException();
            _dbSet.Remove(entity);
        }

        public async Task Delete(Guid id)
        {
            var entity = await GetById(id);
            if (entity == null) throw new NullReferenceException($"{nameof(TEntity)} not found with Id {id}");
            _dbSet.Remove(entity);
        }

        public async Task<IEnumerable<TEntity>> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = _dbSet;
            if (filter != null) query.Where(filter);

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split(
                    new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            if (orderBy != null) return await orderBy(query).ToListAsync();
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAll() => await _dbSet.ToListAsync();

        public async Task<TEntity> GetById(Guid id) => await _dbSet.FindAsync(id);

        public async Task<IEnumerable<TEntity>> GetWithRawSql(string query, params object[] parameters) => await _dbSet.FromSqlRaw(query, parameters).ToListAsync();

        public async Task SaveChangesAsync()
        {
            await _paymentProcessorContext.SaveChangesAsync();
        }

        public void Update(TEntity entity) => _dbSet.Attach(entity).State = EntityState.Modified;
    }
}
