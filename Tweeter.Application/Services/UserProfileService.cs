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
    }
}
