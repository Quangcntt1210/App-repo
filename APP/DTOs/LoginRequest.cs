using System.ComponentModel.DataAnnotations;

namespace APP.DTOs
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Username không được để trống")]
        [StringLength(50, MinimumLength = 3)]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password không được để trống")]
        [StringLength(128)]
        public string Password { get; set; } = string.Empty;
    }
}