using System.ComponentModel.DataAnnotations;

namespace GameRateApp.WebApi.Models.Comment
{
    public class AddCommentRequest
    {
        [Required]
        public string Content { get; set; }
        [Required]
        public int Rate { get; set; }
    }
}
