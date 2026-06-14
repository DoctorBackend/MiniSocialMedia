using Microsoft.EntityFrameworkCore;
using MiniSocialNetworkApi.Data;
using MiniSocialNetworkApi.Models.Domain;

namespace MiniSocialNetworkApi.Repository
{
    public class FollowRepository : IFollowRepository
    {
        private readonly AppDbContext dbContext;

        public FollowRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Follow?> FollowUser(string userId, string followusername)
        {
            if(String.IsNullOrEmpty(followusername)) return null;
            
            var user= await dbContext.Users.FirstOrDefaultAsync(x=>String.Equals(x.UserName, followusername));
            if (user == null) return null;

            if (user.Id == userId) return null; 

            var  alreadyfollows = await dbContext.Follows.AnyAsync(x=>x.FollowerId == userId && x.FollowingId == user.Id);
            if (alreadyfollows) return null;

            var follow = new Follow
            {
                FollowerId = userId,
                FollowingId = user.Id,
            };

            await dbContext.Follows.AddAsync(follow);
            await dbContext.SaveChangesAsync();
            return follow;
        }

        public async Task<List<ApplicationUser>?> GetFollowersAsync(string userId)
        {
            var user = await dbContext.Users.Include(x=>x.Followers).ThenInclude(f => f.Follower).FirstOrDefaultAsync(x=>x.Id==userId);
            if (user == null) return null;

            var followers = user.Followers.Select(x=>x.Follower).ToList();
            return followers;
        }

        public async Task<List<ApplicationUser>?> GetFollowsAsync(string userId)
        {
            var user = await dbContext.Users.Include(x=>x.Followings).ThenInclude(x=>x.Following).FirstOrDefaultAsync(x=>x.Id==userId);
            if (user == null) return null;

            var follows = user.Followings.Select(x=>x.Following).ToList();
            return follows;

        }

        public async Task<Follow?> UnfollowUser(string userId, string unfollowusername)
        {
            if (String.IsNullOrEmpty(unfollowusername)) return null;

            var user = await dbContext.Users.FirstOrDefaultAsync(x => String.Equals(x.UserName, unfollowusername));
            if (user == null) return null;

            var follow = await dbContext.Follows.FirstOrDefaultAsync(x => x.FollowerId == userId && x.FollowingId == user.Id);
            if (follow == null) return null;

            dbContext.Follows.Remove(follow);
            await dbContext.SaveChangesAsync();
            return follow;


        }
    }
}
