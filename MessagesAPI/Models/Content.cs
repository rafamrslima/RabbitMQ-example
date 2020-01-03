using System.ComponentModel.DataAnnotations;

namespace MessagesAPI.Models
{
    public class Content
    {
        [Required]
        public string Message { get; set; }
    }
}
