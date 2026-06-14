using MiniSocialNetworkApi.Models.Domain;
using MiniSocialNetworkApi.Models.DTO;

namespace MiniSocialNetworkApi.Repository
{
    public interface IUserRepository
    {

        Task<ApplicationUser?> GetApplicationUserByIdAsync(string id);

        Task<ApplicationUser?> UpdateApplicationUserAsync(UpdateApplicationUserDto updateuserdto, string userId);

        Task<ApplicationUser?> DeleteAccountAsync(string id);

        Task<List<Post>> GetApplicationUserPosts(string userId);


    }
}
