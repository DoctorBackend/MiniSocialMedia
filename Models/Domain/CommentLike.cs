namespace MiniSocialNetworkApi.Models.Domain
{
    public class CommentLike
    {
        public int Id { get; set; }
        
        public int CommentId { get; set; }
        public Comment Comment { get; set; }

        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
