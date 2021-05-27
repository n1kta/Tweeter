using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Tweeter.Domain.Contracts;

namespace Tweeter.DataAccess.MSSQL.Repositories
{
    public class BaseRepository: IBaseRepository
    {
        protected readonly DbContext _context;

        public BaseRepository(DbContext context)
        {
            _context = context;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public virtual T Get<T>(Expression<Func<T, bool>> predicate = null) where T : class
        {
            return predicate != null ? 
                _context.Set<T>().FirstOrDefault(predicate) 
                : _context.Set<T>().FirstOrDefault(); ;
        }

        public virtual IEnumerable<T> GetAll<T>() where T : class
        {
            return _context.Set<T>().AsNoTracking().ToList();
        }

        public virtual IEnumerable<T> Fetch<T>(Expression<Func<T, bool>> predicate = null) where T : class
        {
            return predicate != null ?
                _context.Set<T>().AsNoTracking().Where(predicate).ToList()
                : _context.Set<T>().AsNoTracking().ToList();
        }

        public IEnumerable<T> GetAllWithInclude<T>(params Expression<Func<T, object>>[] includeProperties) where T : class
        {
            return Include(includeProperties).ToList();
        }

        public IEnumerable<T> GetAllWithInclude<T>(Func<T, bool> predicate, params Expression<Func<T, object>>[] includeProperties) where T : class
        {
            var query = Include(includeProperties);
            return query.Where(predicate).ToList();
        }

        public TEntity GetWithInclude<TEntity>(Func<TEntity, bool> predicate, params Expression<Func<TEntity, object>>[] includeProperties) where TEntity : class
        {
            var query = Include(includeProperties);
            return query.FirstOrDefault(predicate);
        }

        protected IEnumerable<TEntity> Include<TEntity>(params Expression<Func<TEntity, object>>[] includeProperties) where TEntity : class
        {
            var query = _context.Set<TEntity>().AsNoTracking<TEntity>();
            return includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }

        public virtual void Create<T>(T entity) where T : class
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException();

                _context.Set<T>().Add(entity);
                _context.SaveChanges();
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.ToString(), ex);
            }
        }

        public virtual void Remove<T>(T entity) where T : class
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException();

                _context.Set<T>().Remove(entity);
                _context.SaveChanges();
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.ToString(), ex);
            }
        }

        public virtual void Update<T>(T entity) where T : class
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException();

                _context.Entry(entity).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.ToString(), ex);
            }
        }
    }
}
