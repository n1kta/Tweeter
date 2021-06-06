using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly IFileUploader _fileUploader;
        
        public TweetService(IBaseRepository baseRepository,
                            IMapper mapper,
                            IFileUploader fileUploader)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
            _fileUploader = fileUploader;
        }
        
        public ResultHelperModel Create(int userId, TweetDto dto)
        {
            var result = new ResultHelperModel
            {
                IsSuccess = true,
                ErrorMessage = string.Empty
            };
            
            var userProfile = _baseRepository.Get<UserProfile>(x => x.UserId == userId);
            var tweet = _mapper.Map<Tweet>(dto);
            tweet.UserProfile = userProfile;
            tweet.AddedDate = DateTime.Now;

            if (!string.IsNullOrEmpty(dto.Photo))
            {
                try
                {
                    tweet.Photo = _fileUploader.Upload(dto.Photo);
                }
                catch (Exception ex)
                {
                    result.IsSuccess = false;
                    result.ErrorMessage = ex.Message;
                    return result;
                }
            }

            try
            {
                _baseRepository.Create(tweet);
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.ErrorMessage = ex.Message;
                return result;
            }
            
            return result;
        }

        public IEnumerable<TweetDto> GetTweetsFollowers(int userProfileId)
        {
            var followers = _baseRepository.Fetch<Follower>(x => x.FromUser.Id == userProfileId).ToList();
            
            var tweets =
                _baseRepository.GetAllWithInclude<Tweet>(t => followers.Any(x => x.ToUserId == t.UserProfile.Id),
                    x => x.UserProfile,
                    y => y.UserProfile.User).ToList();

            var result = _mapper.Map<IEnumerable<TweetDto>>(tweets);

            return result;
        }
    }
}