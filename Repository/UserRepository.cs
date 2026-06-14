using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MiniSocialNetworkApi.Data;
using MiniSocialNetworkApi.Models.Domain;
using MiniSocialNetworkApi.Models.DTO;

namespace MiniSocialNetworkApi.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext dbContext;

        public UserRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<ApplicationUser?> UpdateApplicationUserAsync(UpdateApplicationUserDto updateuserdto, string userId)
        {
            var user = await GetApplicationUserByIdAsync(userId);
            if(user == null)
            {
                return null;
            }

            var usernameExists = await dbContext.Users.AnyAsync(x=> x.UserName == updateuserdto.UserName);
            if (updateuserdto.UserName != null && !usernameExists)
                user.UserName = updateuserdto.UserName;

            if (updateuserdto.Bio != null)
                user.Bio = updateuserdto.Bio;

           await dbContext.SaveChangesAsync();
           return user;

        }

        public async Task<ApplicationUser?> GetApplicationUserByIdAsync(string id)
        {
            var user = await dbContext.Users.Include(x=>x.Posts)
                .Include(x=>x.Comments).Include(x=>x.PostLikes)
                .Include(x=>x.CommentLikes).Include(x=>x.Followers)
                .Include(x=>x.Followings).FirstOrDefaultAsync(x => x.Id == id);

            return user;
        }

        public async Task<ApplicationUser?> DeleteAccountAsync(string id)
        {

            var user = await GetApplicationUserByIdAsync(id);
            if(user == null)
            {
                return null;
            }
            dbContext.Users.Remove(user);
            await dbContext.SaveChangesAsync();
            return user;

        }

        public Task<List<Post>> GetApplicationUserPosts(string userId)
        {
            var posts = dbContext.Posts.Include(x=>x.ApplicationUser).Include(x=>x.PostLikes).Include(x=>x.Comments).Where(x=>x.ApplicationUserId == userId).ToListAsync();
            return posts;
        }

    }
}
