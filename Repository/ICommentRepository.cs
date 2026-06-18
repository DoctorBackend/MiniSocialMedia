using System.ComponentModel.Design;
using MiniSocialNetworkApi.Models.Domain;
using MiniSocialNetworkApi.Models.DTO;

namespace MiniSocialNetworkApi.Repository
{
    public interface ICommentRepository
    {
        Task<Comment?> AddCommentAsync(string userId, int postId, AddCommentDto commentDto);

        Task<bool> DeleteCommentAsync(string userId, int commentId);

        Task<List<Comment>> GetCommentsForPostAsync(int postId);
        Task<List<CommentLike>> GetCommentLikesAsync(int commentid);

       Task<CommentLike?> LikeCommentAsync(string userId, int commentId);

        Task<bool> DeleteCommentLikeAsync(string userId, int commentlikeId);

    }
}
