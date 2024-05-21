using System.ComponentModel.DataAnnotations;

namespace UserAPI.Models.Dto
{
    public class ResetPasswordDto
    {
        [Required]
        public string Token { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
