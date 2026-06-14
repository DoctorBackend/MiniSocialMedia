using AutoMapper;
using MiniSocialNetworkApi.Models.Domain;
using MiniSocialNetworkApi.Models.DTO;

namespace MiniSocialNetworkApi.AutoMapper
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ApplicationUser, ApplicationUserDto>().ReverseMap();
            CreateMap<Post,PostDto>().ReverseMap();
            CreateMap<Comment,CommentDto>().ReverseMap();
            CreateMap<Follow,FollowDto>().ReverseMap();
            CreateMap<PostLike,PostLikeDto>().ReverseMap();
            CreateMap<CommentLike,CommentLikeDto>().ReverseMap();
        }
       
    }
}
