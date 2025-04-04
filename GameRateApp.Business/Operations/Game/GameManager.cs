using GameRateApp.Business.Operations.Game.Dtos;
using GameRateApp.Business.Types;
using GameRateApp.Data.Entities;
using GameRateApp.Data.Repositories;
using GameRateApp.Data.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameRateApp.Business.Operations.Game
{
    public class GameManager : IGameService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<GameEntity> _gameRepository;

        public GameManager(IUnitOfWork unitOfWork, IRepository<GameEntity> repository)
        {
            _unitOfWork = unitOfWork;
            _gameRepository = repository;
        }
        public async Task<ServiceMessage> AddGame(AddGameDto gameDto)
        {
            var hasGame = _gameRepository.GetAll(x => x.Name.ToLower() == gameDto.Name.ToLower()).Any();

            if (hasGame)
            {
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "This game already exists"
                };
            }

            var gameEntity = new GameEntity
            {
                Name = gameDto.Name,
                Genre = gameDto.Genre,
                Description = gameDto.Description,
                Publisher = gameDto.Publisher,
                PublishDate = gameDto.PublishDate,
                Rate = gameDto.Rate,
                ContentRatingType = gameDto.ContentRatingType,
            };

            _gameRepository.Add(gameEntity);
            
            await _unitOfWork.SaveChangesAsync();


            return new ServiceMessage
            {
                IsSucceed = true,
                Message = "Saved successfully"
            };
        }

        public async Task<ServiceMessage> DeleteGame(int id)
        {
            var gameEntity = _gameRepository.GetById(id);
            if (gameEntity is null)
            {
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "Game is not found"
                };
            }

            _gameRepository.Delete(gameEntity);
            gameEntity.ModifiedDate = DateTime.Now;

            await _unitOfWork.SaveChangesAsync();

            return new ServiceMessage
            {
                IsSucceed = true,
                Message = "Deleted successfully"
            };
        }

        public async Task<List<GameDto>> GetAllGame()
        {
            var games = await _gameRepository.GetAll().Select(x => new GameDto
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Genre = x.Genre,
                Publisher = x.Publisher,
                PublishDate = x.PublishDate,
                Rate = x.Rate,
                ContentRatingType = x.ContentRatingType
            }).ToListAsync();

            return games;
        }

        public async Task<GameDto> GetGame(int id)
        {
            var game = await _gameRepository.GetAll(x => x.Id == id).Select(x => new GameDto
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Genre = x.Genre,
                Publisher = x.Publisher,
                PublishDate = x.PublishDate,
                Rate = x.Rate,
                ContentRatingType = x.ContentRatingType
            }).FirstOrDefaultAsync();

            return game;
        }

        public async Task<ServiceMessage> PatchGame(PatchGameDto gameDto, int id)
        {
            var gameEntity = _gameRepository.GetById(id);
            if (gameEntity is null)
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "Game is not found"
                };

            gameEntity.Description = gameDto.Description;
            gameEntity.Rate = gameDto.Rate;

            gameEntity.ModifiedDate = DateTime.Now;

            _gameRepository.Update(gameEntity);

            await _unitOfWork.SaveChangesAsync();

            return new ServiceMessage
            {
                IsSucceed = true,
                Message = "Patched succesfully"
            };
        }

        public async Task<ServiceMessage> UpdateGame(UpdateGameDto gameDto)
        {
            var gameEntity = _gameRepository.GetById(gameDto.Id);
            if (gameEntity is null)
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "Game is not found"
                };

            gameEntity.Name = gameDto.Name;
            gameEntity.Description = gameDto.Description;
            gameEntity.Genre = gameDto.Genre;
            gameEntity.Publisher = gameDto.Publisher;
            gameEntity.PublishDate = gameDto.PublishDate;
            gameEntity.Rate = gameDto.Rate;
            gameEntity.ContentRatingType = gameDto.ContentRatingType;

            gameEntity.ModifiedDate = DateTime.Now;

            _gameRepository.Update(gameEntity);
            

            await _unitOfWork.SaveChangesAsync();

            return new ServiceMessage
            {
                IsSucceed = true,
                Message = "Updated successfully",
            };
        }

    }
}
