using MiniSocialNetworkApi.Models.Domain;

namespace MiniSocialNetworkApi.Repository
{
    public interface ITokenRepository
    {
        string CreateToken(ApplicationUser user);
    }
}
