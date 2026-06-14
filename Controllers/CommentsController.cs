using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MiniSocialNetworkApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
    }
}


//💬 6. Comment Controller

//👉 Purpose: interaction under posts

//Methods:
//Add comment to post
//Get comments for post
//Update comment
//Delete comment
//Get comment by id (optional)
//👍 7. Comment Like Controller
//Methods:
//Like comment
//Unlike comment
//Get likes of comment


//🧠 8. Optional but real-world useful

//You can also add:

//Feed Controller
//Get posts from people you follow
//Search Controller
//search users/posts
//Media Controller
//upload images for posts