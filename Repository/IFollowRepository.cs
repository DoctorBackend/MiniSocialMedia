using MiniSocialNetworkApi.Models.Domain;

namespace MiniSocialNetworkApi.Repository
{
    public interface IFollowRepository
    {
        Task<List<ApplicationUser>?> GetFollowersAsync(string userId);
        Task<List<ApplicationUser>?> GetFollowsAsync(string userId); 
        Task<Follow?> FollowUser(string userId, string followusername);
        Task<Follow?> UnfollowUser(string userId, string unfollowusername);

    }
}
