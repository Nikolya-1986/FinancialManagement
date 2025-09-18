using System.Text.Json;
using FinancialManagement.Interfaces;
using FinancialManagement.Models.DTO;

namespace FinancialManagement.Services
{
    public class RecaptchaService : IRecaptchaService
    {
        private readonly HttpClient _httpClient;
        private readonly string _secretKey;
        public RecaptchaService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _secretKey = configuration["Recaptcha:SecretKey"];
        }
        public async Task<bool> VerifyAsync(string token)
        {
            var secret = "6Lepqc0rAAAAACDuqYnJiPFdpR23hgmT7efmd2_r";
            var client = new HttpClient();
            var response = await client.PostAsync(
                $"https://www.google.com/recaptcha/api/siteverify?secret={secret}&response={token}",
                null);
            if (!response.IsSuccessStatusCode)
                return false;
            Console.WriteLine(response);
            var jsonString = await response.Content.ReadAsStringAsync();
            var captchaResponse = JsonSerializer.Deserialize<ReCaptchaResponse>(jsonString);
            return captchaResponse.Success && captchaResponse.Score >= 0.5; // Для v3 можно проверить score
        }
    }
}