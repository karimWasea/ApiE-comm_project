using System.ComponentModel.DataAnnotations;

namespace apistudy.Models.Detos
{
    public class WelcomeRequestDto
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Email { get; set; }
    }
}
