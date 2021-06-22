using System;
using System.Linq;
using Tweeter.DataAccess.MSSQL.Entities;

namespace Tweeter.DataAccess.MSSQL.Repositories
{
    public interface ITweeterRepository
    {
        IQueryable<Tweet> GetTweetsWithInclude(Func<Tweet, bool> predicate);
    }
}