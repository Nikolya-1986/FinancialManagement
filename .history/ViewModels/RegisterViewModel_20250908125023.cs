using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinancialManagement.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "Имя должно содержать только буквы.")]
        [Display(Name = "Имя")]
        public string Name { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Некорректный email.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(16, MinimumLength = 8, ErrorMessage = "Пароль должен быть от 8 до 16 символов.")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        [PasswordValidator]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Подтверждение пароля")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают.")]
        public string ConfirmPassword { get; set; }  
    }
}