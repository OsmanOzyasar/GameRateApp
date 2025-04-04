using System.ComponentModel.DataAnnotations;

namespace GameRateApp.WebApi.Models.Game
{
    public class PatchGameRequest
    {
        [Required]
        public string Description { get; set; }
        [Required]
        public int? Rate { get; set; }
    }
}
