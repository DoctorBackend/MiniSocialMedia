using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MiniSocialNetworkApi.Models.Domain;

namespace MiniSocialNetworkApi.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }
        public DbSet<Follow> Follows { get; set; }
        public DbSet<Post> Posts { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<PostImage> PostImages { get; set; }

        public DbSet<PostLike> PostLikes { get; set; }

        public DbSet<CommentLike> CommentLikes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // USER TABLE RULES
           
            // Email must be unique (no two users can register with same email)
            modelBuilder.Entity<ApplicationUser>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // Username must be unique
            modelBuilder.Entity<ApplicationUser>()
                .HasIndex(u => u.UserName)
                .IsUnique();


         
            // POST ↔ USER RELATIONSHIP
        
            // One User → Many Posts
            // If user is deleted → delete all their posts
            modelBuilder.Entity<Post>()
                .HasOne(p => p.ApplicationUser)
                .WithMany(u => u.Posts)
                .HasForeignKey(p => p.ApplicationUserId)
                .OnDelete(DeleteBehavior.Cascade);


            // COMMENT ↔ POST RELATIONSHIP

            // One Post → Many Comments
            // If post is deleted → delete all its comments
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Post)
                .WithMany(p => p.Comments)
                .HasForeignKey(c => c.PostId)
                .OnDelete(DeleteBehavior.Cascade);


            // =========================
            // COMMENT ↔ USER RELATIONSHIP
            // =========================

            // One User → Many Comments
            // If user is deleted → delete all their comments
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.ApplicationUser)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.ApplicationUserId)
                .OnDelete(DeleteBehavior.Cascade);


            // POSTLIKE ↔ POST RELATIONSHIP

            // One Post → Many PostLikes
            // If post is deleted → delete all likes on that post
            modelBuilder.Entity<PostLike>()
                .HasOne(pl => pl.Post)
                .WithMany(p => p.PostLikes)
                .HasForeignKey(pl => pl.PostId)
                .OnDelete(DeleteBehavior.Cascade);


            // POSTLIKE ↔ USER RELATIONSHIP

            // One User → Many PostLikes
            // If user is deleted → delete all their post likes
            modelBuilder.Entity<PostLike>()
                .HasOne(pl => pl.ApplicationUser)
                .WithMany(u => u.PostLikes)
                .HasForeignKey(pl => pl.ApplicationUserId)
                .OnDelete(DeleteBehavior.Cascade);


            // UNIQUE RULE: POST LIKE

            // A user can like a post ONLY ONCE
            modelBuilder.Entity<PostLike>()
                .HasIndex(l => new { l.ApplicationUserId, l.PostId })
                .IsUnique();


            // COMMENTLIKE ↔ COMMENT RELATIONSHIP

            // One Comment → Many CommentLikes
            // If comment is deleted → delete all likes on that comment
            modelBuilder.Entity<CommentLike>()
                .HasOne(cl => cl.Comment)
                .WithMany(c => c.Likes)
                .HasForeignKey(cl => cl.CommentId)
                .OnDelete(DeleteBehavior.Cascade);


            // COMMENTLIKE ↔ USER RELATIONSHIP

            // One User → Many CommentLikes
            // If user is deleted → delete all their comment likes
            modelBuilder.Entity<CommentLike>()
                .HasOne(cl => cl.ApplicationUser)
                .WithMany(u => u.CommentLikes)
                .HasForeignKey(cl => cl.ApplicationUserId)
                .OnDelete(DeleteBehavior.Cascade);


            // UNIQUE RULE: COMMENT LIKE

            // A user can like a comment ONLY ONCE
            modelBuilder.Entity<CommentLike>()
                .HasIndex(l => new { l.ApplicationUserId, l.CommentId })
                .IsUnique();



            modelBuilder.Entity<Follow>()
                .HasOne(f=>f.Follower)
                .WithMany(u=>u.Followings)
                .HasForeignKey(f=>f.FollowerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Follow>()
                .HasOne(f=> f.Following)
                .WithMany(u => u.Followers)
                .HasForeignKey(f => f.FollowingId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Follow>()
                .HasIndex(f => new { f.FollowerId, f.FollowingId })
                .IsUnique();
        }
    }
}
