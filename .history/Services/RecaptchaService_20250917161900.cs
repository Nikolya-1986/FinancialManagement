using FinancialManagement.Interfaces;

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
            var response = await _httpClient.GetAsync(
                $"https://www.google.com/recaptcha/api/siteverify?secret={_secretKey}&response={token}");

            if (!response.IsSuccessStatusCode)
                return false;

            var json = await response.Content.ReadAsStringAsync();
            dynamic data = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
            return data.success == true;
        }
    }
}