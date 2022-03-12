using AutoMapper;
using MCVTask.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MCVTask.Infrastructure.Base
{
    public abstract class EntityRepository<T> where T : class
    {
        protected AppDbContext AppDbContext { get; }
        protected IMapper Mapper { get; }
        internal EntityRepository(AppDbContext appdbContext, IMapper mapper)
        {
            AppDbContext = appdbContext;
            Mapper = mapper;
        }
        public async Task<List<T>> GetPageAsync<TKey>(int skipCount, int takeCount, Expression<Func<T, bool>> filter, Expression<Func<T, TKey>> sortingExpression, string includeProperties = "")
        {
            IQueryable<T> query = AppDbContext.Set<T>();
            skipCount *= takeCount;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            query = includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            if (skipCount == 0)
                query = query.OrderByDescending(sortingExpression).Take(takeCount);
            else
                query = query.OrderByDescending(sortingExpression).Skip(skipCount).Take(takeCount);
            
            return await query.AsNoTracking().ToListAsync();
        }
        public async Task<int> GetCountAsync(Expression<Func<T, bool>> filter)
        {
            return await AppDbContext.Set<T>().Where(filter).CountAsync();
        }
        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> filter = null, string includeProperties = "")
        {
            IQueryable<T> query = AppDbContext.Set<T>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            query = includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            return await query.FirstOrDefaultAsync();
        }
        public void CreateAsyn(T entity)
        {
            AppDbContext.Set<T>().AddAsync(entity);
        }
        public void Update(T entity)
        {
            AppDbContext.Set<T>().Update(entity);
        }
        public void Delete(T entity)
        {
            AppDbContext.Set<T>().Remove(entity);
        }
    }
}
