using GameRateApp.Business.Operations.User.Dtos;
using GameRateApp.Business.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameRateApp.Business.Operations.User
{
    public interface IAuthService
    {
        Task<ServiceMessage> AddUser(AddUserDto addUserDto);
        ServiceMessage<UserInfoDto> LoginUser(LoginUserDto loginUserDto);
        Task<ServiceMessage> UpdateUser(EditProfileDto editProfileDto, int currentUserId);
        Task<UserDto> GetUser(int userId);
    }
}
