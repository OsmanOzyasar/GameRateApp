using GameRateApp.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameRateApp.Business.Operations.Game.Dtos
{
    public class GameDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }
        public DateTime PublishDate { get; set; }
        public string Publisher { get; set; }
        public string Description { get; set; }
        public int? Rate { get; set; }
        public ContentRatingType? ContentRatingType { get; set; }
    }
}
