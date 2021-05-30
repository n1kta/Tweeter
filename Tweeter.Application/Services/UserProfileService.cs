using System;
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

        private const string CAN_NOT_FOLLOW_YOURSELF = "You can't follow to yourself.";

        public UserProfileService(IBaseRepository baseRepository,
            IMapper mapper)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
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
            var isFollowed = entity != null;

            try
            {
                if (isFollowed)
                {
                    UnFollow(entity);
                }
                else
                {
                    Follow(dto);
                }
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.ErrorMessage = ex.Message;
            }
            
            return result;
        }

        private void Follow(FollowDto dto)
        {
            var follow = _mapper.Map<Follower>(dto);
            _baseRepository.Create<Follower>(follow);
        }

        private void UnFollow(Follower entity)
        {
            _baseRepository.Remove<Follower>(entity);
        }
    }
}
