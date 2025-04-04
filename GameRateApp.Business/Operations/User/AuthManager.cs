using GameRateApp.Business.DataProtection;
using GameRateApp.Business.Operations.Game;
using GameRateApp.Business.Operations.User.Dtos;
using GameRateApp.Business.Types;
using GameRateApp.Data.Entities;
using GameRateApp.Data.Enums;
using GameRateApp.Data.Repositories;
using GameRateApp.Data.UnitOfWork;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameRateApp.Business.Operations.User
{
    public class AuthManager : IAuthService
    {
        private readonly IRepository<UserEntity> _userRepository;
        private readonly IRepository<UserGameEntity> _userGameRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDataProtection _dataProtector;
        private readonly IServiceProvider _serviceProvider;

        public AuthManager(IRepository<UserEntity> userRepository, IUnitOfWork unitOfWork, IDataProtection protector, IRepository<UserGameEntity> userGameRepository, IServiceProvider serviceProvider)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _userGameRepository = userGameRepository;
            _dataProtector = protector;
            _serviceProvider = serviceProvider;
        }
        public async Task<ServiceMessage> AddUser(AddUserDto addUserDto)
        {
            var hasUser = _userRepository.GetAll(x => x.UserName.ToLower() == addUserDto.UserName.ToLower()).Any();
            if (hasUser)
            {
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "This username is already in use"
                };
            }

            var gameRepo = _serviceProvider.GetRequiredService<IRepository<GameEntity>>();

            await _unitOfWork.BeginTransectionAsync();

            var protectedPassword = _dataProtector.Protect(addUserDto.Password);

            var userEntity = new UserEntity
            {
                UserName = addUserDto.UserName,
                Password = protectedPassword,
                Email = addUserDto.Email,
                FirstName = addUserDto.FirstName,
                LastName = addUserDto.LastName,
                BirthDate = addUserDto.BirthDate,
                RoleType = RoleType.User,

            };

            _userRepository.Add(userEntity);

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {
                await _unitOfWork.RollBackTransectionAsync();
                throw new Exception("An error occured while saving the user");
            }

            foreach (var gameId in addUserDto.GameIds)
            {
                var userGame = new UserGameEntity
                {
                    UserId = userEntity.Id,
                    GameId = gameId,
                };

                var game = gameRepo.GetById(gameId);

                if(!(game is null))
                    _userGameRepository.Add(userGame);
                else 
                    continue;
            }

            try
            {
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransectionAsync();
            }
            catch (Exception)
            {
                await _unitOfWork.RollBackTransectionAsync();
                throw new Exception("An error occured while adding the user's game");
            }

            return new ServiceMessage
            {
                IsSucceed = true,
                Message = "User added successfully"
            };
        }

        public async Task<UserDto> GetUser(int userId)
        {
            var user = await _userRepository.GetAll(x => x.Id == userId).Select(x => new UserDto
            {
                Id = x.Id,
                UserName = x.UserName,
                UserGames = x.Games.Select(f => new UserGameDto
                {
                    Id = f.Game.Id,
                    Name = f.Game.Name
                }).ToList(),
            }).FirstOrDefaultAsync();
            return user;
        }

        public ServiceMessage<UserInfoDto> LoginUser(LoginUserDto loginUserDto)
        {
            var userEntity = _userRepository.Get(x => x.UserName.ToLower() == loginUserDto.UserName.ToLower());

            if (userEntity is null)
            {
                return new ServiceMessage<UserInfoDto>
                {
                    IsSucceed = false,
                    Message = "Username or password is incorrect"
                };
            }

            var unprotectedPassword = _dataProtector.UnProtect(userEntity.Password);

            if (loginUserDto.Password != unprotectedPassword)
            {
                return new ServiceMessage<UserInfoDto>
                {
                    IsSucceed = false,
                    Message = "Username or password is incorrect"
                };
            }
            else
            {
                return new ServiceMessage<UserInfoDto>
                {
                    IsSucceed = true,
                    Message = "Success",
                    Data = new UserInfoDto
                    {
                        Id = userEntity.Id,
                        UserName = loginUserDto.UserName,
                        Email = userEntity.Email,
                        FirstName = userEntity.FirstName,
                        LastName = userEntity.LastName,
                        RoleType = userEntity.RoleType
                    }
                };
            }
        }

        public async Task<ServiceMessage> UpdateUser(EditProfileDto editProfileDto, int currentUserId)
        {
            var user = _userRepository.GetById(currentUserId);
            if (user is null)
            {
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "User is not found"
                };
            }

            var hasUser = _userRepository.GetAll(x => x.UserName.ToLower() == editProfileDto.UserName.ToLower()).Any();
            var gameRepo = _serviceProvider.GetRequiredService<IRepository<GameEntity>>();
            

            if (hasUser)
            {
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "This username is already in use"
                };
            }

            await _unitOfWork.BeginTransectionAsync();

            user.UserName = editProfileDto.UserName;
            user.Email = editProfileDto.Email;
            user.FirstName = editProfileDto.FirstName;
            user.LastName = editProfileDto.LastName;
            user.BirthDate = editProfileDto.BirthDate;

            user.ModifiedDate = DateTime.Now;

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {
                await _unitOfWork.RollBackTransectionAsync();
                throw new Exception("An error occured while editing the user");
            }

            var userGames = _userGameRepository.GetAll(x => x.UserId == user.Id).ToList();

            foreach (var userGame in userGames)
            {
                _userGameRepository.Delete(userGame, false);
            }

            foreach (var gameId in editProfileDto.GameIds)
            {
                var userGame = new UserGameEntity
                {
                    UserId = user.Id,
                    GameId = gameId,
                };

                var game = gameRepo.GetById(gameId);

                if(!(game is null))
                    _userGameRepository.Add(userGame);
                else
                    continue;
            }

            try
            {
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransectionAsync();
            }
            catch (Exception)
            {
                await _unitOfWork.RollBackTransectionAsync();
                throw new Exception("An error occured while editing the user");
            }

            return new ServiceMessage
            {
                IsSucceed = true,
                Message = "Edited successfully"
            };
        }
    }
}
