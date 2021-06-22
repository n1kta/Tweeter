using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Tweeter.DataAccess.MSSQL.Entities;

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
                .Where(predicate)
                .OrderByDescending(x => x.AddedDate)
                .AsQueryable();

            return query;
        }
    }
}