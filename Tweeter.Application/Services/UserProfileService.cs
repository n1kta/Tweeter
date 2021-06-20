using System;
using System.Linq;
using AutoMapper;
using Tweeter.DataAccess.MSSQL.Entities;
using Tweeter.Domain.Contracts;
using Tweeter.Domain.Dtos;
using Tweeter.Domain.HelperModels;

namespace Tweeter.Application.Services
{
    public class UserProfileService : IUserProfileService
    {
        private readonly IBaseRepository _baseRepository;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        private const string CAN_NOT_FOLLOW_YOURSELF = "You can't follow to yourself.";

        public UserProfileService(IBaseRepository baseRepository,
            IMapper mapper,
            IUserService userService)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
            _userService = userService;
        }

        public ResultHelperModel Create(int userId, UserProfileDto dto)
        {
            var userProfile = _mapper.Map<UserProfile>(dto);
            userProfile.UserId = userId;
            userProfile.AddedDate = DateTime.Now;
            
            var result = new ResultHelperModel
            {
                IsSuccess = true,
                ErrorMessage = string.Empty
            };

            try
            {
                _baseRepository.Create(userProfile);
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

        public ResultHelperModel Update(int userId, UserProfileDto dto)
        {
            var userProfile = _baseRepository.Get<UserProfile>(x => x.UserId == userId);
            
            _mapper.Map(dto, userProfile);
            
            var result = new ResultHelperModel
            {
                IsSuccess = true,
                ErrorMessage = string.Empty
            };
            
            try
            {
                _baseRepository.Update(userProfile);
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

        public ResultHelperModel ToggleFollow(FollowDto dto)
        {
            var result = new ResultHelperModel
            {
                IsSuccess = true,
                ErrorMessage = string.Empty
            };

            if (dto.SourceId == dto.DestinationId)
            {
                result.IsSuccess = false;
                result.ErrorMessage = CAN_NOT_FOLLOW_YOURSELF;

                return result;
            }

            var entity =
                _baseRepository.Get<Follower>(x => x.FromUserId == dto.SourceId
                                                   && x.ToUserId == dto.DestinationId);
            
            try
            {
                if (entity != null)
                {
                    _baseRepository.Remove<Follower>(entity);
                }
                else
                {
                    var follow = _mapper.Map<Follower>(dto);
                    _baseRepository.Create<Follower>(follow);
                }
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.ErrorMessage = ex.Message;
            }
            
            return result;
        }

        public FollowerDto GetFollowerFollowing(int userProfileId)
        {
            var followers = _baseRepository.Fetch<Follower>(x => x.ToUserId == userProfileId).ToList();
            var followings = _baseRepository.Fetch<Follower>(x => x.FromUserId == userProfileId).ToList();

            var currentUserProfile = _userService.GetCurrentUser();
            var isFollowed =
                _baseRepository.Get<Follower>(x => x.FromUserId == currentUserProfile.UserProfile.Id
                                                   && x.ToUserId == userProfileId) != null;

            var result = new FollowerDto
            {
                Followers = followers.Count(),
                Followings = followings.Count(),
                IsCurrentUserProfileFollowed = isFollowed
            };

            return result;
        }
    }
}
