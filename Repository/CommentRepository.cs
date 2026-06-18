using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;
using MiniSocialNetworkApi.Data;
using MiniSocialNetworkApi.Models.Domain;
using MiniSocialNetworkApi.Models.DTO;

namespace MiniSocialNetworkApi.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly AppDbContext dbContext;

        public CommentRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Comment?> AddCommentAsync(string userId, int postId,AddCommentDto commentDto)
        {
            var postexists = await dbContext.Posts.AnyAsync(x => x.Id == postId);
            if(postexists == false) return null;


            var comment = new Comment
            {
                PostId = postId,
                ApplicationUserId = userId,
                Text = commentDto.Text,
            };

            await dbContext.Comments.AddAsync(comment);
            await dbContext.SaveChangesAsync();
            return comment;
        }

        public async Task<bool> DeleteCommentLikeAsync(string userId, int commentlikeId)
        {
            var commentlike = await dbContext.CommentLikes.FirstOrDefaultAsync(x=>x.ApplicationUserId==userId && x.Id==commentlikeId);
            if(commentlike == null) return false;

            dbContext.CommentLikes.Remove(commentlike);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteCommentAsync(string userId, int commentId)
        {
            var comment = await dbContext.Comments.FirstOrDefaultAsync(x=>x.Id == commentId && x.ApplicationUserId == userId);
            if (comment == null) return false;

            dbContext.Comments.Remove(comment);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<Comment>> GetCommentsForPostAsync(int postId)
        {
            var comments = await dbContext.Comments.Where(x=>x.PostId == postId).ToListAsync();
            return comments;
        }

        public async Task<CommentLike?> LikeCommentAsync(string userId, int commentId)
        {
            var commentexists = await dbContext.Comments.AnyAsync(x => x.Id == commentId);
            var likeexists = await dbContext.CommentLikes.AnyAsync(x=>x.ApplicationUserId== userId && x.CommentId == commentId);
            
            if(!commentexists )
                return null;

            if(likeexists)
                return null;

            var commentlike = new CommentLike
            {
                CommentId = commentId,
                ApplicationUserId = userId
            };
            await dbContext.CommentLikes.AddAsync(commentlike);
            await dbContext.SaveChangesAsync();
            return commentlike;
        }

        public async Task<List<CommentLike>> GetCommentLikesAsync(int commentid)
        {
            var commetlikes = await dbContext.CommentLikes.Where(x=>x.CommentId == commentid).ToListAsync();
            return commetlikes;
        }
    }
}
