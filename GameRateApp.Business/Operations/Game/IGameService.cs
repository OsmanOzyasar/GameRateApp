using GameRateApp.Business.Operations.Game.Dtos;
using GameRateApp.Business.Types;
using GameRateApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameRateApp.Business.Operations.Game
{
    public interface IGameService
    {
        Task<ServiceMessage> AddGame(AddGameDto gameDto);
        Task<GameDto> GetGame(int id);
        Task<List<GameDto>> GetAllGame();
        Task<ServiceMessage> UpdateGame(UpdateGameDto gameDto);
        Task<ServiceMessage> DeleteGame(int id);
        Task<ServiceMessage> PatchGame(PatchGameDto gameDto, int id);
    }
}
