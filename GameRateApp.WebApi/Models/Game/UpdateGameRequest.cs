using GameRateApp.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace GameRateApp.WebApi.Models.Game
{
    public class UpdateGameRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Genre { get; set; }

        [Required]
        public DateTime PublishDate { get; set; }
        [Required]
        public string Publisher { get; set; }
        [Required]
        public string Description { get; set; }
        public int Rate { get; set; }
        [Required]
        public ContentRatingType ContentRatingType { get; set; }
    }
}
