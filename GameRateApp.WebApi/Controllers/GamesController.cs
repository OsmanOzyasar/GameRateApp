using GameRateApp.Business.Operations.Game;
using GameRateApp.Business.Operations.Game.Dtos;
using GameRateApp.Data.Entities;
using GameRateApp.Data.Repositories;
using GameRateApp.Data.UnitOfWork;
using GameRateApp.WebApi.Filters;
using GameRateApp.WebApi.Models.Game;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GameRateApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GamesController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpPost("/add-game")]
        [Authorize(Roles = "Admin")]
        [TimeControlFilter]
        public async Task<IActionResult> AddGame(AddGameRequest request)
        {
            var addGameDto = new AddGameDto
            {
                Name = request.Name,
                Genre = request.Genre,
                Description = request.Description,
                Publisher = request.Publisher,
                PublishDate = request.PublishDate,
                Rate = request.Rate,
                ContentRatingType = request.ContentRatingType,
            };

            var result = await _gameService.AddGame(addGameDto); 

            if(result.IsSucceed)
                return Ok(result.Message);
            else
                return BadRequest(result.Message);
        }

        [HttpGet("{id}/get-game")]
        public async Task<IActionResult> GetGame(int id)
        {
            var game = await _gameService.GetGame(id);

            if(game is null)
                return NotFound();
            else
                return Ok(game);
        }

        [HttpGet("/get-games")]
        public async Task<IActionResult> GetGames()
        {
            var games = await _gameService.GetAllGame();
            return Ok(games);
        }

        [HttpPut("{id}/update-game-info")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateGame(UpdateGameRequest request, int id)
        {
            var gameDto = new UpdateGameDto
            {
                Id = id,
                Name = request.Name,
                Genre = request.Genre,
                Description = request.Description,
                Publisher = request.Publisher,
                PublishDate = request.PublishDate,
                Rate = request.Rate,
                ContentRatingType = request.ContentRatingType,
            };

            var result = await _gameService.UpdateGame(gameDto);

            if(result.IsSucceed)
                return await GetGame(id);
            else
                return NotFound(result.Message);
        }

        [HttpPatch("{id}/edit-game")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PatchGame(PatchGameRequest request, int id)
        {
            var gameDto = new PatchGameDto
            {
                Description = request.Description,
                Rate = request.Rate,
            };

            var result = await _gameService.PatchGame(gameDto, id);
            if (result.IsSucceed) 
                return Ok(result.Message);
            else
                return NotFound(result.Message);
        }

        [HttpDelete("{id}/delete")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteGame(int id)
        {
            var result = await _gameService.DeleteGame(id);

            if (result.IsSucceed) 
                return Ok(result.Message);
            else 
                return NotFound(result.Message);
        }
    }
}
