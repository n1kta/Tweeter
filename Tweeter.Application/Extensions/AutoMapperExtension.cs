using System;
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
                .ForAllMembers(entity => entity.Condition((src, dest, srcMember) => srcMember != null && (string) srcMember != ""));
            CreateMap<TweetDto, Tweet>();
            CreateMap<FollowDto, Follower>()
                .ForMember(entity => entity.FromUserId, src => src.MapFrom(x => x.SourceId))
                .ForMember(entity => entity.ToUserId, src => src.MapFrom(x => x.DestinationId));

            // From Entity to Dto
            CreateMap<UserProfile, UserProfileDto>()
                .ForAllMembers(dto => dto.Condition((src, dest, srcMember) => srcMember != null && (string) srcMember != ""));
            CreateMap<User, UserDto>()
                .ForMember(dto => dto.UserProfile, src => src.MapFrom(x => x.UserProfile));
            CreateMap<User, ViewProfileDto>()
                .ForMember(dto => dto.Following, src => src.Ignore())
                .ForMember(dto => dto.Followers, src => src.Ignore());
        }
    }
}
