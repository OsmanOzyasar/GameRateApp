using GameRateApp.Data.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GameRateApp.WebApi.Models.User
{
    public class AddUserRequest
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [PasswordPropertyText]
        public string Password { get; set; }
        [Required]
        public List<int> GameIds { get; set; }
    }
}
