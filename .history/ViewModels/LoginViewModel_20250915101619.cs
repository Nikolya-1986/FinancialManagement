using System.ComponentModel.DataAnnotations;

namespace FinancialManagement.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "Incorrect email.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(16, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 16 characters.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [PasswordValidator]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}