using System;
using System.Collections.Generic;
using System.Linq;
using Tweeter.DataAccess.MSSQL.Entities;

namespace Tweeter.DataAccess.MSSQL.Repositories.Contracts
{
    public interface ITweeterRepository
    {
        IQueryable<Tweet> GetTweetsWithInclude(Func<Tweet, bool> predicate);
    }
}