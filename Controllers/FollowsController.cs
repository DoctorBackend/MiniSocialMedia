using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniSocialNetworkApi.Models.Domain;
using MiniSocialNetworkApi.Models.DTO;
using MiniSocialNetworkApi.Repository;

namespace MiniSocialNetworkApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FollowsController : ControllerBase
    {
        private readonly IFollowRepository repository;
        private readonly IMapper mapper;

        public FollowsController(IFollowRepository repository , IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }


        [HttpGet("followers")]
        public async Task<IActionResult> GetAllFollowers()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
                return Unauthorized("User not authenticated");

            var followers = await repository.GetFollowersAsync(userId);

            if (followers == null)
                return Ok("There are no any follows");

            return Ok(mapper.Map<List<ApplicationUserDto>>(followers));
        }

        [HttpGet("followings")]
        public async Task<IActionResult> GetAllFollows()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if(userId==null)
                return Unauthorized("User not authenticated");

            var follows = await repository.GetFollowsAsync(userId);

            if (follows == null)
                return Ok("There are no any follows");

            return Ok(mapper.Map<List<ApplicationUserDto>>(follows));
        }
        

        [HttpPost("follow/{followUserName}")]
        public async Task<IActionResult> FollowUserAsync([FromRoute] string followUserName )
        {
           var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if(userId==null)
                return Unauthorized("User not authenticated");

            var follow = await repository.FollowUser(userId, followUserName);
            if (follow == null)
                return BadRequest("Follow operation failed");

            return Ok(mapper.Map<FollowDto>(follow));



        }

        [HttpDelete("unfollow/{unfollowUserName}")]
        public async Task<IActionResult> UnFollowUserAsync([FromRoute] string unfollowUserName)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
                return Unauthorized("User not authenticated");
            
            var unfollow = await repository.UnfollowUser(userId, unfollowUserName);
            if (unfollow == null)
                return BadRequest("Unfollow operation failed");

            return Ok("User succesfully deleted from Follows");

        }



    }
}


//🧑‍🤝‍🧑 3. Follow Controller (VERY IMPORTANT for social media)

//👉 Purpose: social graph(Instagram logic)

//Methods:
//Follow user
//current user → follows another user
//Unfollow user
//Get followers of a user
//Get users that I am following
