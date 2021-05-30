﻿using System;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Tweeter.Application.Helpers;
using Tweeter.DataAccess.MSSQL.Entities;
using Tweeter.Domain.Contracts;
using Tweeter.Domain.Dtos;

namespace Tweeter.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IBaseRepository _baseRepository;
        private readonly IMapper _mapper;
        private readonly IJwtService _jwtService;

        private const string AUTHORIZATION_HEADER = "Authorization";

        public UserService(IHttpContextAccessor httpContextAccessor,
                           IBaseRepository baseRepository,
                           IMapper mapper,
                           IJwtService jwtService)
        {
            _httpContextAccessor = httpContextAccessor;
            _baseRepository = baseRepository;
            _mapper = mapper;
            _jwtService = jwtService;
        }

        public UserDto GetCurrentUser()
        {
            var user = GetCurrentUserByToken();

            _mapper.Map<UserProfileDto>(user.UserProfile);

            var result = _mapper.Map<UserDto>(user);

            return result;
        }

        public ViewProfileDto GetViewProfileByUserName(string userName)
        {
            var user = _baseRepository.GetWithInclude<User>(u => u.UserName == userName, x => x.UserProfile);

            if (user == null)
                throw new Exception("This user doesn't exist.");

            _mapper.Map<UserProfileDto>(user.UserProfile);
            
            var result = _mapper.Map<ViewProfileDto>(user);
            result.Following = 2569;
            result.Followers = 10080;
            
            return result;
        }

        private User GetCurrentUserByToken()
        {
            var token = _httpContextAccessor.HttpContext.Request.Headers[AUTHORIZATION_HEADER];

            var userName = _jwtService.DecodeToken(token);

            var result = _baseRepository.GetWithInclude<User>(x => x.UserName == userName, u => u.UserProfile);

            return result;
        }
    }
}