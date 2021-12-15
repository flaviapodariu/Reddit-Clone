using Microsoft.EntityFrameworkCore;
using Wreddit.Entities;

namespace Wreddit.Data
{
    public class WredditContext: DbContext
    {
        public WredditContext(DbContextOptions<WredditContext> options) : base(options) { }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Comments> Comments { get; set; }
        public DbSet<User> Users { get; set; }


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

            base.OnModelCreating(modelBuilder);
        }


    }
}
