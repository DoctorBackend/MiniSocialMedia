using MiniSocialNetworkApi.Models.Domain;

namespace MiniSocialNetworkApi.Models.DTO
{
    public class PostDto
    {

        public int Id { get; set; }

        public string Content { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string ApplicationUserId { get; set; }

        public ApplicationUserDto ApplicationUser { get; set; }

        public string? ImageUrl { get; set; }

        public List<PostLikeDto> PostLikes { get; set; } = new List<PostLikeDto>();

        public List<CommentDto> Comments { get; set; } = new List<CommentDto>();
    }
}
