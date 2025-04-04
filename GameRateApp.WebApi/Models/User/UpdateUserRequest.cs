using System.ComponentModel.DataAnnotations;

namespace GameRateApp.WebApi.Models.User
{
    public class UpdateUserRequest
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }

        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Required]
        public List<int> GameIds { get; set; }
    }
}
