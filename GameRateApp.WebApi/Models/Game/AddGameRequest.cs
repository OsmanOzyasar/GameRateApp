using GameRateApp.Data.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GameRateApp.WebApi.Models.Game
{
    public class AddGameRequest
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
