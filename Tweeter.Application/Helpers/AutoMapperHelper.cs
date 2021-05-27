using AutoMapper;
using Tweeter.DataAccess.MSSQL.Entities;
using Tweeter.Domain.Dtos;

namespace Tweeter.Application.Helpers
{
    public class AutoMapperHelper : Profile
    {
        public AutoMapperHelper()
        {
            // From Dto to Entity
            CreateMap<UserProfileDto, UserProfile>()
                .ForAllMembers(dto => dto.Condition((src, dest, srcMember) => srcMember != null && (string) srcMember != ""));


            // From Entity to Dto
            CreateMap<UserProfile, UserProfileDto>()
                .ForAllMembers(dto => dto.Condition((src, dest, srcMember) => srcMember != null && (string) srcMember != ""));
            CreateMap<User, UserInfoDto>()
                .ForMember(dto => dto.UserProfile, src => src.MapFrom(x => x.UserProfile));
        }
    }
}
