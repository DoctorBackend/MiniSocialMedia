using System.Security.Claims;
using AutoMapper;
using AutoMapper.Configuration.Annotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniSocialNetworkApi.Models.DTO;
using MiniSocialNetworkApi.Repository;

namespace MiniSocialNetworkApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentRepository repository;
        private readonly IMapper mapper;

        public CommentsController(ICommentRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpPost("posts/{postId:int}/comments")]
        public async Task<IActionResult> AddCommentToPost([FromRoute] int postId, [FromBody] AddCommentDto commentDto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
                return Unauthorized("User not authenticated");

            if (String.IsNullOrEmpty(commentDto.Text))
                return BadRequest("Comment text cannot be empty");

            if (postId <=0)
                return BadRequest("Invalid postId");


            var comment = await repository.AddCommentAsync(userId, postId, commentDto);
            if(comment == null)
                return BadRequest("Oops something went wrong");

            return Ok(mapper.Map<CommentDto>(comment));

        }


        [HttpGet("posts/{postId:int}/comments")]
        public async Task<IActionResult> GetCommentsForPost([FromRoute] int postId)
        {
            var comments = await repository.GetCommentsForPostAsync(postId);
            return Ok(mapper.Map<List<CommentDto>>(comments));
        }


        [HttpDelete("{commentId:int}")]
        public async Task<IActionResult> DeleteComment([FromRoute] int commentId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
                return Unauthorized("User not authenticated");

            var commentisdeleted = await repository.DeleteCommentAsync(userId, commentId);

            if (!commentisdeleted)
                return BadRequest("Oops something went wrong");

            return Ok();


        }


        [HttpPost("{commentId:int}/likes")]
        public async Task<IActionResult> LikeComment([FromRoute] int commentId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
                return Unauthorized("User not authenticated");

            var commentlike = await repository.LikeCommentAsync(userId, commentId);
            if (commentlike == null)
                return BadRequest("Oops something went wrong");

            return Ok(mapper.Map<CommentLikeDto>(commentlike));

        }


        [HttpDelete("likes/{commentLikeId:int}")]
        public async Task<IActionResult> DeleteLikeComment([FromRoute] int commentLikeId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
                return Unauthorized("User not authenticated");
            var likeistaken = await repository.DeleteCommentLikeAsync(userId,commentLikeId);
            if (!likeistaken)
                return BadRequest("Oops something went wrong");
            return Ok();

        }

        [HttpGet("{commentId:int}/likes")]
        public async Task<IActionResult> GetCommentLikes([FromRoute] int commentId)
        {
            var commentlikes = await repository.GetCommentLikesAsync(commentId);

            return Ok(mapper.Map<List<CommentLikeDto>>(commentlikes));
        }
    }
}

