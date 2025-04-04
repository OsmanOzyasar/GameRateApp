using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameRateApp.Business.Operations.Game.Dtos
{
    public class PatchGameDto
    {
        public string Description { get; set; }
        public int? Rate { get; set; }
    }
}
