using Azure.Core;
using GameRateApp.Business.Operations.User;
using GameRateApp.Business.Operations.User.Dtos;
using GameRateApp.WebApi.Jwt;
using GameRateApp.WebApi.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GameRateApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Reagister(AddUserRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userDto = new AddUserDto
            {
                UserName = request.UserName,
                Password = request.Password,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                BirthDate = request.BirthDate,
                GameIds = request.GameIds,
            };

            var result = await _authService.AddUser(userDto);

            if (result.IsSucceed)
                return Ok(result.Message);
            else
                return BadRequest(result.Message);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _authService.LoginUser(new LoginUserDto
            {
                UserName = request.UserName,
                Password = request.Password
            });

            if (!result.IsSucceed)
            {
                return BadRequest(result.Message);
            }

            var user = result.Data;

            var config = HttpContext.RequestServices.GetRequiredService<IConfiguration>();
            var token = JwtHelper.GenerateJwtToken(new JwtDto
            {
                Id = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                RoleType = user.RoleType,
                SecretKey = config["Jwt:SecretKey"]!,
                Issuer = config["Jwt:Issuer"]!,
                Audience = config["Jwt:Audience"]!,
                ExpireMinute = int.Parse(config["Jwt:ExpireMinutes"]!)

            });

            return Ok(new LoginResponse
            {
                Token = token,
                Message = "Success"
            });
        }

        [HttpPut("edit-profile")]
        [Authorize]
        public async Task<IActionResult> UpdateUser(UpdateUserRequest request)
        {
            var currentUserId = int.Parse(User.FindFirst("Id")?.Value!);

            var userDto = new EditProfileDto
            {
                UserName = request.UserName,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                BirthDate = request.BirthDate,
                GameIds = request.GameIds,
            };

            var result = await _authService.UpdateUser(userDto, currentUserId);

            if(result.IsSucceed)
            {
                return Ok(result.Message);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        [HttpGet("{id}/user-details")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _authService.GetUser(id);

            if(user is null)
            {
                return NotFound();
            }
            else
            {
                return Ok(user);
            }
        }
    }
}
