namespace MiniSocialNetworkApi.Models.Domain
{
    public class Post
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public string? ImageUrl { get; set; }

        public List<PostLike> PostLikes { get; set; } = new List<PostLike>();

        public List<Comment> Comments { get; set; } = new List<Comment>();

    }
}
