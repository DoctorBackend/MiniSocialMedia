namespace MiniSocialNetworkApi.Models.Domain
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
        public List<CommentLike> Likes { get; set; } = new List<CommentLike>();
    }
}
