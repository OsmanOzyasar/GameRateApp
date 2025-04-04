using GameRateApp.Business.Operations.Comment;
using GameRateApp.Business.Operations.Comment.Dtos;
using GameRateApp.WebApi.Models.Comment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GameRateApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet("{gameId}/get-comment/{userId}")]
        public async Task<IActionResult> Get(int gameId, int userId)
        {

            var result = await _commentService.GetComment(gameId, userId);

            if (result is null)
                return NotFound();
            else
                return Ok(result);
        }

        [HttpGet("{gameId}/get-game-comments")]
        public async Task<IActionResult> GetAll(int gameId)
        {
            var result = await _commentService.GetAllComments(gameId);

            if (result is null || result.Count == 0)
                return NotFound();
            else
                return Ok(result);
        }

        [HttpPost("{gameId}/add-comment")]
        [Authorize]
        public async Task<IActionResult> AddComment(int gameId, AddCommentRequest request)
        {
            var currentUserId = int.Parse(User.FindFirst("Id")?.Value!);

            var commentDto = new AddCommentDto
            {
                Content = request.Content,
                Rate = request.Rate,
            };

            var result = await _commentService.AddComment(currentUserId, gameId, commentDto);

            if (result.IsSucceed)
                return Ok(result.Message);
            else
                return NotFound(result.Message);
        }

        [HttpDelete("{gameId}/delete-comment")]
        [Authorize]
        public async Task<IActionResult> DeleteComment(int gameId)
        {
            var currentUserId = int.Parse(User.FindFirst("Id")?.Value!);

            var result = await _commentService.DeleteComment(gameId, currentUserId);

            if (result.IsSucceed) 
                return Ok(result.Message);
            else
                return NotFound(result.Message);
        }

        [HttpPatch("{gameId}/edit-comment")]
        [Authorize]
        public async Task<IActionResult> EditComment(int gameId, EditCommentRequest request)
        {
            var currentUserId = int.Parse(User.FindFirst("Id")?.Value!);

            var commentDto = new EditCommentDto
            {
                Content = request.Content,
                Rate = request.Rate,
            };

            var result = await _commentService.EditComment(commentDto, currentUserId, gameId);

            if (result.IsSucceed) 
                return Ok(result.Message);
            else 
                return NotFound(result.Message);
        }

    }
}
