using System.ComponentModel.DataAnnotations;

namespace GameRateApp.WebApi.Models.Comment
{
    public class EditCommentRequest
    {
        [Required]
        public string Content { get; set; }
        [Required]
        public int Rate { get; set; }
    }
}
