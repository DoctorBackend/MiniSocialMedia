namespace MiniSocialNetworkApi.Models.Domain
{
    public class PostLike
    {
        public int Id { get; set; }
        
        public int PostId { get; set; }
        public Post Post { get; set; }  
   
        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

    }
}
