using MiniSocialNetworkApi.Models.Domain;

namespace MiniSocialNetworkApi.Models.DTO
{
    public class CommentDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string ApplicationUserId { get; set; }
        public ApplicationUserDto ApplicationUser { get; set; }
        public int PostId { get; set; }
        public PostDto Post { get; set; }
        public List<CommentLikeDto> Likes { get; set; } = new List<CommentLikeDto>();
    }
}
