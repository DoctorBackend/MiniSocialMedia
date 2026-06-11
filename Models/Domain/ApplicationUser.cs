using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace MiniSocialNetworkApi.Models.Domain
{
    public class ApplicationUser : IdentityUser
    {
        public string? Bio { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public List<Follow> Followers { get; set; } = new List<Follow>();
        public List<Follow> Followings { get; set; } = new List<Follow>();
        public List<Post> Posts { get; set; } = new List<Post>();
        public List<Comment> Comments { get; set; } = new List<Comment>();
        public List<PostLike> PostLikes { get; set; } = new List<PostLike>();
        public List<CommentLike> CommentLikes { get; set; } = new List<CommentLike>();

    }
}