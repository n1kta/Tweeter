using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Tweeter.Domain.Contracts;

namespace Tweeter.DataAccess.MSSQL.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly DbContext _context;
        private readonly DbSet<TEntity> _entities;

        public BaseRepository(DbContext context)
        {
            _context = context;
            _entities = context.Set<TEntity>();
        }

        public TEntity FindById(int id)
        {
            return _entities.Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _entities.AsNoTracking().ToList();
        }

        public IEnumerable<TEntity> Fetch(Func<TEntity, bool> predicate)
        {
            return _entities.AsNoTracking().Where(predicate).ToList();
        }

        public void Create(TEntity entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException();

                _entities.Add(entity);
                _context.SaveChanges();
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.ToString(), ex);
            }
        }

        public void Remove(TEntity entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException();

                _entities.Remove(entity);
                _context.SaveChanges();
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.ToString(), ex);
            }
        }

        public void Update(TEntity entity)
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
