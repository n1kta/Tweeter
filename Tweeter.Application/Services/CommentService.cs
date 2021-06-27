using System;
using AutoMapper;
using Tweeter.DataAccess.MSSQL.Entities;
using Tweeter.DataAccess.MSSQL.Repositories.Contracts;
using Tweeter.Domain.Contracts;
using Tweeter.Domain.Dtos;
using Tweeter.Domain.HelperModels;

namespace Tweeter.Application.Services
{
    public class CommentService : ICommentService, ILikeService
    {
        private readonly IBaseRepository _baseRepository;
        private readonly IMapper _mapper;
        
        public CommentService(IBaseRepository baseRepository,
                                IMapper mapper)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
        }
        
        public ResultHelperModel AddComment(CreatCommentDto dto)
        {
            var result = new ResultHelperModel
            {
                Success = true,
                Message = string.Empty
            };

            try
            {
                var comment = _mapper.Map<Comment>(dto);
                comment.AddedDate = DateTime.Now;

                _baseRepository.Create(comment);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }
            
            return result;
        }
        
        public ResultHelperModel ToggleLike(LikeDto dto)
        {
            var result = new ResultHelperModel
            {
                Success = true,
                Message = string.Empty
            };

            var entity = _baseRepository.Get<CommentLike>(x =>
                x.UserProfileId == dto.UserProfileId && x.CommentId == dto.DestinationId);

            try
            {
                if (entity != null)
                {
                    _baseRepository.Remove(entity);
                }
                else
                {
                    var like = _mapper.Map<CommentLike>(dto);
                    _baseRepository.Create(like);
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }

            return result;
        }
    }
}