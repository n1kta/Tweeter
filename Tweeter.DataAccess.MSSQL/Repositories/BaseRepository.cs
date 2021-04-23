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
        private readonly DbContext _context;

        public BaseRepository(DbContext context)
        {
            _context = context;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public T Get<T>(Expression<Func<T, bool>> predicate = null) where T : class
        {
            return predicate != null ? 
                _context.Set<T>().FirstOrDefault(predicate) 
                : _context.Set<T>().FirstOrDefault(); ;
        }

        public IEnumerable<T> GetAll<T>() where T : class
        {
            return _context.Set<T>().AsNoTracking().ToList();
        }

        public IEnumerable<T> Fetch<T>(Expression<Func<T, bool>> predicate = null) where T : class
        {
            return predicate != null ?
                _context.Set<T>().AsNoTracking().Where(predicate).ToList()
                : _context.Set<T>().AsNoTracking().ToList();
        }

        public void Create<T>(T entity) where T : class
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

        public void Remove<T>(T entity) where T : class
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

        public void Update<T>(T entity) where T : class
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
