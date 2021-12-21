using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Wreddit.Models.Entities;

namespace Wreddit.Data
{
    public class WredditContext : IdentityDbContext<User, Role, int,
        IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>,
        IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public WredditContext(DbContextOptions options) : base(options) { }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        //public DbSet<User> Users { get; set; }
        //public DbSet<Role> Roles { get; set; }
        //public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<CommentVotes> CommentVotes { get; set; }

        public DbSet<PostVotes> PostVotes { get; set; }

        public DbSet<SessionToken> SessionTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // One(Post) to Many(Comments)
            modelBuilder.Entity<Post>()
                .HasMany(p => p.Comments)
                .WithOne(c => c.Post);

            //One(User) to Many(Comments)
            modelBuilder.Entity<User>()
                .HasMany(c => c.Comments)
                .WithOne(u => u.User);

            //One(User) to Many(Posts)
            modelBuilder.Entity<User>()
                .HasMany(p => p.Posts)
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

            


        }
    }
}
