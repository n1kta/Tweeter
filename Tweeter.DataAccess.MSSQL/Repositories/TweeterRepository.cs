using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Tweeter.DataAccess.MSSQL.Entities;
using Tweeter.DataAccess.MSSQL.Repositories.Contracts;

namespace Tweeter.DataAccess.MSSQL.Repositories
{
    public class TweeterRepository : BaseRepository, ITweeterRepository
    {
        public TweeterRepository(DbContext context) : base(context)
        {
        }

        public IQueryable<Tweet> GetTweetsWithInclude(Func<Tweet, bool> predicate)
        {
            var query = _context.Set<Tweet>().AsNoTracking()
                .Include(up => up.UserProfile)
                .Include(u => u.UserProfile.User)
                .Include(t => t.TweetLikes)
                .Include(c => c.Comments)
                    .ThenInclude(x => x.UserProfile)
                    .ThenInclude(x => x.CommentLikes)
                .Where(predicate)
                .OrderByDescending(x => x.AddedDate)
                .AsQueryable();

            return query;
        }
    }
}