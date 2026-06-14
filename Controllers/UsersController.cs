using System.Runtime.InteropServices;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.VisualBasic;
using MiniSocialNetworkApi.Models.Domain;
using MiniSocialNetworkApi.Models.DTO;
using MiniSocialNetworkApi.Repository;

namespace MiniSocialNetworkApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository repository;
        private readonly IMapper mapper;

        public UsersController(IUserRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("{id:string}")]
        public async Task<IActionResult> GetUserById([FromRoute] string id)
        {
            var user = await repository.GetApplicationUserByIdAsync(id);
            if (user == null)
                return NotFound("User not found");
            return Ok(mapper.Map<ApplicationUserDto>(user));
        }

        [HttpGet]
        [Route("me")]
        public async Task<IActionResult> GetCurrentUserProfile()
        {
            var userid = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userid == null)
                return Unauthorized("User not authenticated");

            var currentuser = await repository.GetApplicationUserByIdAsync(userid);
            if (currentuser == null)
                return NotFound("User not found");
            return Ok(mapper.Map<ApplicationUserDto>(currentuser));


        }

        [HttpPut]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateApplicationUserDto updateUserProfileDto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
                return Unauthorized("User not authenticated");
            var user = await repository.UpdateApplicationUserAsync(updateUserProfileDto, userId);
            if (user == null)
                return NotFound("User not found");

            return Ok(mapper.Map<ApplicationUserDto>(user));
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteAccount()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if(userId == null)
                return Unauthorized("User not authenticated");
            var deletedUser = await repository.DeleteAccountAsync(userId);
            if (deletedUser == null)
                return NotFound("User not found");

            return Ok("Account deleted successfully");

        }

        [HttpGet]
        public async Task<IActionResult> GetUserPosts()
        { 
            var userid  = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userid == null)
                return Unauthorized("User not authenticated");

            var userposts = await repository.GetApplicationUserPosts(userid);

            return Ok(mapper.Map<List<PostDto>>(userposts));

        }

    }
}




















//👤 2. User Controller

//👉 Purpose: user profile &social actions

//Methods:
//Get user profile by id  +
//Get current user profile +
//Update profile (bio, fullname, etc.) +
//Delete account +
//Get user posts +
//Get user followers +
//Get user following +






