using System.ComponentModel.DataAnnotations;

namespace FinancialManagement.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "The name must contain only letters.")]
        [Display(Name = "Name")]
        public string Name { get; set; }

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

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "The passwords do not match.")]
        public string ConfirmPassword { get; set; }  
    }
}