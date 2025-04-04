using GameRateApp.Business.Operations.Comment.Dtos;
using GameRateApp.Business.Operations.Game.Dtos;
using GameRateApp.Business.Types;
using GameRateApp.Data.Entities;
using GameRateApp.Data.Repositories;
using GameRateApp.Data.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameRateApp.Business.Operations.Comment
{
    public class CommentManager : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<CommentEntity> _commentRepository;
        private readonly IRepository<GameEntity> _gameRepository;

        public CommentManager(IUnitOfWork unitOfWork, IRepository<CommentEntity> commentRepository, IRepository<GameEntity> gameRepository)
        {
            _unitOfWork = unitOfWork;
            _commentRepository = commentRepository;
            _gameRepository = gameRepository;
        }
        public async Task<ServiceMessage> AddComment(int userId, int gameId, AddCommentDto commentDto)
        {
            var gameEntity = _gameRepository.GetById(gameId);
            if (gameEntity is null)
            {
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "Game is not found"
                };
            }

            var comment = new CommentEntity
            {
                UserId = userId,
                GameId = gameId,
                Content = commentDto.Content,
                Rate = commentDto.Rate,
            };

            _commentRepository.Add(comment);

            await _unitOfWork.SaveChangesAsync();

            return new ServiceMessage
            {
                IsSucceed = true,
                Message = "Added successfully"
            };
        }

        public async Task<ServiceMessage> DeleteComment(int gameId, int userId)
        {
            var commentEntity = _commentRepository.GetAll(x => x.UserId == userId && x.GameId == gameId).FirstOrDefault();

            if (commentEntity is null)
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "Comment is not found"
                };

            _commentRepository.Delete(commentEntity);
            commentEntity.ModifiedDate = DateTime.Now;

            await _unitOfWork.SaveChangesAsync();

            return new ServiceMessage
            {
                IsSucceed = true,
                Message = "Deleted successfully"
            };
        }

        public async Task<ServiceMessage> EditComment(EditCommentDto commentDto, int userId, int gameId)
        {
            var commentEntity = _commentRepository.GetAll(x => x.UserId == userId && x.GameId == gameId).FirstOrDefault();

            if (commentEntity is null)
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "Comment is not found"
                };

            commentEntity.Content = commentDto.Content;
            commentEntity.Rate = commentDto.Rate;

            commentEntity.ModifiedDate = DateTime.Now;

            await _unitOfWork.SaveChangesAsync();

            return new ServiceMessage
            {
                IsSucceed = true,
                Message = "Edited successfully"
            };
        }

        public async Task<List<CommentDto>> GetAllComments(int gameId)
        {
            var game = _gameRepository.GetById(gameId);
            var gameName = game?.Name ?? "Unknown Game";
            var gameComments = await _commentRepository.GetAll(x => x.GameId == gameId)
                                                 .Select(x => new CommentDto
                                                 {
                                                     GameName = gameName,
                                                     Content = x.Content,
                                                     Rate = x.Rate,
                                                 }).ToListAsync();

            return gameComments;
           
        }

        public async Task<CommentDto> GetComment(int gameId, int userId)
        {
            var game = _gameRepository.GetById(gameId);
            var gameName = game?.Name ?? "Unknown Game";
            var commentEntity = await _commentRepository.GetAll(x => x.UserId == userId && x.GameId == gameId)
                                                  .Select(x => new CommentDto
                                                  {
                                                      GameName = gameName,
                                                      Content = x.Content,
                                                      Rate = x.Rate,
                                                  }).FirstOrDefaultAsync();                                                
            return commentEntity;
        }
    }
}
