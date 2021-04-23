using Microsoft.EntityFrameworkCore;
using Tweeter.DataAccess.MSSQL.Entities;

namespace Tweeter.DataAccess.MSSQL.Context {
    public class TweeterContext : DbContext
    {
        public TweeterContext(DbContextOptions<TweeterContext> options) : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }

        public DbSet<UserProfile> UserProfiles { get; set; }
    }
}
