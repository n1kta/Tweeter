using System;
using System.Collections.Generic;

namespace Tweeter.Domain.Contracts
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        TEntity FindById(int id);

        IEnumerable<TEntity> GetAll();

         /// <summary>
         /// Filter entity by property
         /// </summary>
         /// <typeparam name="TEntity"></typeparam>
         /// <param name="predicate"></param>
         /// <returns></returns>
         IEnumerable<TEntity> Fetch(Func<TEntity, bool> predicate);

        void Create(TEntity entity);

         void Remove(TEntity entity);

         void Update(TEntity entity);
    }
}
