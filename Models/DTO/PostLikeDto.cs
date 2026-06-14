using MiniSocialNetworkApi.Models.Domain;

namespace MiniSocialNetworkApi.Models.DTO
{
    public class PostLikeDto
    {
        public int Id { get; set; }

        public int PostId { get; set; }
        public PostDto Post { get; set; }

        public string ApplicationUserId { get; set; }

        public ApplicationUserDto ApplicationUser { get; set; }
    }
}
