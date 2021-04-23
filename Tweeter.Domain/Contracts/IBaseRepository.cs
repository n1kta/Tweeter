using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Tweeter.Domain.Contracts
{
    public interface IBaseRepository
    {
        TEntity Get<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class;

        IEnumerable<TEntity> GetAll<TEntity>() where TEntity : class;

         /// <summary>
         /// Filter entity by property
         /// </summary>
         /// <typeparam name="TEntity"></typeparam>
         /// <param name="predicate"></param>
         /// <returns></returns>
         IEnumerable<TEntity> Fetch<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class;

        void Create<TEntity>(TEntity entity) where TEntity : class;

         void Remove<TEntity>(TEntity entity) where TEntity : class;

         void Update<TEntity>(TEntity entity) where TEntity : class;

         void SaveChanges();
    }
}
