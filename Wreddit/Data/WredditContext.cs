using Microsoft.EntityFrameworkCore;
using Wreddit.Models.Entities;

namespace Wreddit.Data
{
    public class WredditContext : DbContext
    {
        public WredditContext(DbContextOptions<WredditContext> options) : base(options) { }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<CommentVotes> CommentVotes { get; set; }

        public DbSet<PostVotes> PostVotes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // One(Post) to Many(Comments)
            modelBuilder.Entity<Post>()
                .HasMany(p => p.Comments)
                .WithOne(c => c.Post);

            //One(User) to Many(Comments)
            modelBuilder.Entity<User>()
                .HasMany(c => c.Comments)
                .WithOne(u => u.User);

            modelBuilder.Entity<UserRole>(ur =>
            {
                ur.HasKey(ur => new { ur.UserId, ur.RoleId });

                ur.HasOne(ur => ur.Role).WithMany(r => r.UserRoles).HasForeignKey(ur => ur.RoleId);
                ur.HasOne(ur => ur.User).WithMany(u => u.UserRoles).HasForeignKey(ur => ur.UserId);
            });

            modelBuilder.Entity<PostVotes>(ur =>
            {
                ur.HasKey(ur => new { ur.PostId, ur.UserId });

                ur.HasOne(ur => ur.Post).WithMany(r => r.PostVotes).HasForeignKey(ur => ur.PostId);
                ur.HasOne(ur => ur.User).WithMany(u => u.PostVotes).HasForeignKey(ur => ur.UserId);


            });

            modelBuilder.Entity<CommentVotes>(ur =>
            {
                ur.HasKey(ur => new { ur.CommentId, ur.UserId });

                ur.HasOne(ur => ur.Comment).WithMany(r => r.CommentsVotes).HasForeignKey(ur => ur.CommentId);
                ur.HasOne(ur => ur.User).WithMany(u => u.CommentsVotes).HasForeignKey(ur => ur.UserId);
            });

            base.OnModelCreating(modelBuilder);


        }
    }
}
