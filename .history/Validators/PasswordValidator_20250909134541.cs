using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

public class PasswordValidator : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        string password = value as string;

        if (string.IsNullOrEmpty(password))
        {
            return new ValidationResult("Password cannot be empty.");
        }

        if (!Regex.IsMatch(password, @"^[A-Za-z0-9!@#$%^&*()_+\-=\{\};':""\\|,.<>\/?]+$"))
        {
            return new ValidationResult("The password contains invalid characters.");
        }

        if (!Regex.IsMatch(password, @"[A-Z]"))
        {
            return new ValidationResult("The password must contain at least one capital letter.");
        }

        if (!Regex.IsMatch(password, @"[a-z]"))
        {
            return new ValidationResult("The password must contain at least one lowercase letter.");
        }

        if (!Regex.IsMatch(password, @"\d"))
        {
            return new ValidationResult("The password must contain at least one number.");
        }

        if (!Regex.IsMatch(password, @"[!@#$%^&*()_+\-=\{\};':""\\|,.<>\/?]"))
        {
            return new ValidationResult("The password must contain at least one special character.");
        }

        return ValidationResult.Success;
    }
}