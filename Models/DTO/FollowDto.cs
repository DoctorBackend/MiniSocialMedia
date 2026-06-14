using MiniSocialNetworkApi.Models.Domain;

namespace MiniSocialNetworkApi.Models.DTO
{
    public class FollowDto
    {
        public int Id { get; set; }
        public string FollowerId { get; set; }
        public ApplicationUserDto Follower { get; set; }


        public string FollowingId { get; set; }
        public ApplicationUserDto Following { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
