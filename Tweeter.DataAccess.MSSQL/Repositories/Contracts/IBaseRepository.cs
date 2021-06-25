using System;
using System.Linq;
using System.Linq.Expressions;

namespace Tweeter.DataAccess.MSSQL.Repositories.Contracts
{
    public interface IBaseRepository
    {
        TEntity Get<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class;

        IQueryable<TEntity> GetAll<TEntity>() where TEntity : class;

         /// <summary>
         /// Filter entity by property
         /// </summary>
         /// <typeparam name="TEntity"></typeparam>
         /// <param name="predicate"></param>
         /// <returns></returns>
         IQueryable<TEntity> Fetch<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class;

         IQueryable<TEntity> GetAllWithInclude<TEntity>(params Expression<Func<TEntity, object>>[] includeProperties) where TEntity : class;

         IQueryable<TEntity> GetAllWithInclude<TEntity>(Func<TEntity, bool> predicate, params Expression<Func<TEntity, object>>[] includeProperties) where TEntity : class;

        TEntity GetWithInclude<TEntity>(Func<TEntity, bool> predicate, params Expression<Func<TEntity, object>>[] includeProperties) where TEntity : class;

        void Create<TEntity>(TEntity entity) where TEntity : class;

         void Remove<TEntity>(TEntity entity) where TEntity : class;

         void Update<TEntity>(TEntity entity) where TEntity : class;

         void SaveChanges();
    }
}
