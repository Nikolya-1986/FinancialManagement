using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

public class PasswordValidator : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        string password = value as string;

        if (string.IsNullOrEmpty(password))
        {
            return new ValidationResult("Пароль не может быть пустым.");
        }

        // Проверка на разрешённые символы
        if (!Regex.IsMatch(password, @"^[A-Za-z0-9!@#$%^&*()_+\-=\{\};':""\\|,.<>\/?]+$"))
        {
            return new ValidationResult("Пароль содержит недопустимые символы.");
        }

        // Проверка наличия заглавной буквы
        if (!Regex.IsMatch(password, @"[A-Z]"))
        {
            return new ValidationResult("Пароль должен содержать хотя бы одну заглавную букву.");
        }

        // Проверка наличия строчной буквы
        if (!Regex.IsMatch(password, @"[a-z]"))
        {
            return new ValidationResult("Пароль должен содержать хотя бы одну строчную букву.");
        }

        // Проверка наличия цифры
        if (!Regex.IsMatch(password, @"\d"))
        {
            return new ValidationResult("Пароль должен содержать хотя бы одну цифру.");
        }

        // Проверка наличия специального символа
        if (!Regex.IsMatch(password, @"[!@#$%^&*()_+\-=\{\};':""\\|,.<>\/?]"))
        {
            return new ValidationResult("Пароль должен содержать хотя бы один специальный символ.");
        }

        // Если все проверки пройдены
        return ValidationResult.Success;
    }
}