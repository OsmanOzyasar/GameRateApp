using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameRateApp.Business.Operations.Comment.Dtos
{
    public class CommentDto
    {
        public string GameName { get; set; }
        public string Content { get; set; }
        public int Rate { get; set; }
    }
}
