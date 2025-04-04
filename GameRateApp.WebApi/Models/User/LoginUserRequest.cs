using System.ComponentModel.DataAnnotations;

namespace GameRateApp.WebApi.Models.User
{
    public class LoginUserRequest
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
