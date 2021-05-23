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

        public DbSet<Tweet> Tweets { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<TweetLike> TweetLikes { get; set; }

        public DbSet<CommentLike> CommentLikes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Many to Many for Likes for Posts
            modelBuilder.Entity<TweetLike>()
                .HasKey(k => new {k.UserProfileId, k.TweetId});

            modelBuilder.Entity<TweetLike>()
                .HasOne(s => s.UserProfile)
                .WithMany(l => l.TweetLikes)
                .HasForeignKey(s => s.UserProfileId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<TweetLike>()
                .HasOne(s => s.Tweet)
                .WithMany(l => l.TweetLikes)
                .HasForeignKey(s => s.TweetId)
                .OnDelete(DeleteBehavior.NoAction);

            // Many to Many for Likes for Comments
            modelBuilder.Entity<CommentLike>()
                .HasKey(k => new { k.UserProfileId, k.CommentId });

            modelBuilder.Entity<CommentLike>()
                .HasOne(s => s.UserProfile)
                .WithMany(l => l.CommentLikes)
                .HasForeignKey(s => s.UserProfileId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<CommentLike>()
                .HasOne(s => s.Comment)
                .WithMany(l => l.CommentLikes)
                .HasForeignKey(s => s.CommentId)
                .OnDelete(DeleteBehavior.NoAction);

            // Many to Many for Followers
            modelBuilder.Entity<Follower>()
                .HasKey(k => new { k.FromUserId, k.ToUserId });

            modelBuilder.Entity<Follower>()
                .HasOne(s => s.FromUser)
                .WithMany(l => l.Followings)
                .HasForeignKey(s => s.FromUserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Follower>()
                .HasOne(s => s.ToUser)
                .WithMany(l => l.Followers)
                .HasForeignKey(s => s.ToUserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
