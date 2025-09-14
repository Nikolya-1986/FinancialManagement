using FinancialManagement.Data;

namespace FinancialManagement.Interfaces
{
    public interface IUserRepository
    {
        Task<ApplicationUser> GetByIdAsync(string id);
        Task<ApplicationUser> GetByEmailAsync(string email);
        Task<bool> CreateUserAsync(ApplicationUser user, string password);
        Task AddToRoleAsync(ApplicationUser user, string role);
    }
}