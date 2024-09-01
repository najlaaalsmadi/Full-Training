using System.ComponentModel.DataAnnotations;

namespace Backend.DTO
{
    public class UsersDTO
    {

        [Required(ErrorMessage ="username is required")]
        [MaxLength(50)]
        //[Required(ErrorMessage = "Username is required.")]
        //[StringLength(50, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 50 characters.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }
    }
}
