using Microsoft.AspNetCore.Identity;

namespace FinancialManagement.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public Guid UniqueId { get; set; } = Guid.NewGuid();        public Tariff Tariff { get; set; } = Tariff.Simple;
    }
}