namespace FinancialManagement.Models.DTO
{
    public class ReCaptchaResponse
    {
        public bool Success { get; set; }
        public DateTime Challenge_ts { get; set; }
        public string Hostname { get; set; }
        public List<string> ErrorCodes { get; set; }
        public float Score { get; set; } // Для v3 
    }
}