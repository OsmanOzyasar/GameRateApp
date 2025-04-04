using GameRateApp.Business.Operations.Comment.Dtos;
using GameRateApp.Business.Operations.Game.Dtos;
using GameRateApp.Business.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameRateApp.Business.Operations.Comment
{
    public interface ICommentService
    {
        Task<ServiceMessage> AddComment(int userId, int gameId, AddCommentDto commentDto);
        Task<CommentDto> GetComment(int gameId, int userId);
        Task<List<CommentDto>> GetAllComments(int gameId);
        Task<ServiceMessage> DeleteComment(int gameId, int userId);
        Task<ServiceMessage> EditComment(EditCommentDto commentDto, int userId, int gameId);
    }
}
