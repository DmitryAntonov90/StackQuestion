using System.ComponentModel.DataAnnotations;

namespace StackQuestion.ViewModels.Account
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "User name")]
        public string Login { get; set; }

        [Required]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Confirm email")]
        [DataType(DataType.EmailAddress)]
        [Compare("Email", ErrorMessage = "Field confirm email must match confirm email")]
        public string ConfirmEmail { get; set; }

        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Confirm password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Confirm Password must be equal Password")]
        public string ConfirmPassword { get; set; }
    }
}
