using System.Linq;
using AutoMapper;
using Tweeter.DataAccess.MSSQL.Entities;
using Tweeter.Domain.Dtos;

namespace Tweeter.Application.Extensions
{
    public class AutoMapperExtension : Profile
    {
        public AutoMapperExtension()
        {
            // From Dto to Entity
            CreateMap<UserProfileDto, UserProfile>()
                .ForAllMembers(entity => entity.Condition((src, dest, srcMember) => srcMember != null && srcMember.ToString() != ""));
            CreateMap<TweetDto, Tweet>()
                .ForMember(entity => entity.Photo, src => src.Ignore());
            CreateMap<FollowDto, Follower>()
                .ForMember(entity => entity.FromUserId, src => src.MapFrom(x => x.SourceId))
                .ForMember(entity => entity.ToUserId, src => src.MapFrom(x => x.DestinationId));
            CreateMap<LikeDto, TweetLike>()
                .ForMember(entity => entity.TweetId, src => src.MapFrom(x => x.DestinationId));
            CreateMap<LikeDto, CommentLike>()
                .ForMember(entity => entity.CommentId, src => src.MapFrom(x => x.DestinationId));
            CreateMap<CommentDto, Comment>();

            // From Entity to Dto
            CreateMap<UserProfile, UserProfileDto>()
                .ForAllMembers(dto => dto.Condition((src, dest, srcMember) => srcMember != null && srcMember.ToString() != ""));
            CreateMap<User, UserDto>()
                .ForMember(dto => dto.UserProfile, src => src.MapFrom(x => x.UserProfile));
            CreateMap<User, ViewProfileDto>();
            CreateMap<Comment, CommentDto>()
                .ForMember(dto => dto.UserProfile, src => src.MapFrom(x => x.UserProfile))
                .ForMember(dto => dto.Likes, src => src.MapFrom(x => x.CommentLikes.Count));
            CreateMap<Tweet, TweetDto>()
                .ForMember(dto => dto.UserName, src => src.MapFrom(x => x.UserProfile.User.UserName))
                .ForMember(dto => dto.Likes, src => src.MapFrom(x => x.TweetLikes.Count))
                .ForMember(dto => dto.Comments, src => src.MapFrom(x => x.Comments.ToList()));
        }
    }
}
