using MiniSocialNetworkApi.Models.Domain;

namespace MiniSocialNetworkApi.Models.DTO
{
    public class ApplicationUserDto
    {
        public string? Bio { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public List<FollowDto> Followers { get; set; } = new List<FollowDto>();
        public List<FollowDto> Followings { get; set; } = new List<FollowDto>();
        public List<PostDto> Posts { get; set; } = new List<PostDto>();
        public List<CommentDto> Comments { get; set; } = new List<CommentDto>();
        public List<PostLikeDto> PostLikes { get; set; } = new List<PostLikeDto>();
        public List<CommentLikeDto> CommentLikes { get; set; } = new List<CommentLikeDto>();
    }
}
