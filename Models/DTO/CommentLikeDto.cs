using MiniSocialNetworkApi.Models.Domain;

namespace MiniSocialNetworkApi.Models.DTO
{
    public class CommentLikeDto
    {
        public int Id { get; set; }

        public int CommentId { get; set; }
        public CommentDto Comment { get; set; }

        public string ApplicationUserId { get; set; }

        public ApplicationUserDto ApplicationUser { get; set; }
    }
}
