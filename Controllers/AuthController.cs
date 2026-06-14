using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using MiniSocialNetworkApi.Models.Domain;
using MiniSocialNetworkApi.Models.DTO;
using MiniSocialNetworkApi.Repository;

namespace MiniSocialNetworkApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ITokenRepository repository;
        private readonly UserManager<ApplicationUser> usermanager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AuthController(ITokenRepository repository, UserManager<ApplicationUser> usermanager,SignInManager<ApplicationUser> signInManager)
        {
            this.repository = repository;
            this.usermanager = usermanager;
            this.signInManager = signInManager;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterRequestDto userdto)
        {
            var user = new ApplicationUser
            {
                UserName = userdto.UserName,
                Email = userdto.UserName,
                Bio = userdto.Bio,
            };
            var identityresult = await usermanager.CreateAsync(user, userdto.Password);
            if (identityresult.Succeeded)
            {
                return Ok("User Registered Successfully");
            }
            else
            {
                return BadRequest(identityresult.Errors);
            }

        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserLoginRequestDto userdto)
        {
              var user = await usermanager.FindByEmailAsync(userdto.UserName);
              if (user == null)
                    return NotFound("User not found");

              var result  = await signInManager.CheckPasswordSignInAsync(
                    user,
                    userdto.Password, 
                    lockoutOnFailure:true);

              if (result.IsLockedOut)
                    return StatusCode(423,"The account lockedout for teh password requests");


              if (!result.Succeeded)
                    return Unauthorized("Invalid credentials");

              var token = repository.CreateToken(user);
              var newttokenDto = new TokenDto
                {
                    Token = token
                };
              return Ok(newttokenDto);

        }
    }
}



