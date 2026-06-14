using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MiniSocialNetworkApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
    }
}


//👉 Purpose: content system

//Methods:
//Create post
//Get all posts (feed)
//Get post by id
//Get posts by user
//Update post
//Delete post
//❤️ 5. Post Like Controller (or inside PostController)

//👉 Purpose: reactions

//Methods:
//Like post
//Unlike post
//Get likes of a post
//Check if user liked post (optional)