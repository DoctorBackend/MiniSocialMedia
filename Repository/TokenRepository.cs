using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using MiniSocialNetworkApi.Models.Domain;

namespace MiniSocialNetworkApi.Repository
{
    public class TokenRepository : ITokenRepository
    {
        private IConfiguration configuration;

        public TokenRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string CreateToken(ApplicationUser user)
        {
            var Claims = new List<Claim>();
            Claims.Add(new Claim(ClaimTypes.Email, user.Email));
            Claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials  =  new SigningCredentials(key , SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken
            (
                audience: configuration["Jwt:Audience"],
                issuer: configuration["Jwt:Issuer"],
                claims: Claims,
                expires:DateTime.UtcNow.AddMinutes(15),
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
