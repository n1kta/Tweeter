using System;
using AutoMapper;
using Tweeter.DataAccess.MSSQL.Entities;
using Tweeter.Domain.Contracts;
using Tweeter.Domain.Dtos;
using Tweeter.Domain.HelperModels;

namespace Tweeter.Application.Services
{
    public class TweetService : ITweetService
    {
        private readonly IBaseRepository _baseRepository;
        private readonly IMapper _mapper;
        
        public TweetService(IBaseRepository baseRepository,
                            IMapper mapper)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
        }
        
        public ResultHelperModel Create(int userId, TweetDto dto)
        {
            var userProfile = _baseRepository.Get<UserProfile>(x => x.UserId == userId);

            var tweet = _mapper.Map<Tweet>(dto);
            tweet.UserProfile = userProfile;
            tweet.AddedDate = DateTime.Now;
            
            var result = new ResultHelperModel
            {
                IsSuccess = true,
                ErrorMessage = string.Empty
            };

            try
            {
                _baseRepository.Create(tweet);
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