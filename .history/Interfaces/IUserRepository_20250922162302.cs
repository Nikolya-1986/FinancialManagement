using FinancialManagement.Data;

namespace FinancialManagement.Interfaces
{
    public interface IUserRepository
    {
        Task<List<ApplicationUser>> GetAllAsync();
        Task<ApplicationUser> GetByIdAsync(string id);
        Task<ApplicationUser> GetByEmailAsync(string email);
        Task<bool> CreateUserAsync(ApplicationUser user, string password);
        Task AddToRoleAsync(ApplicationUser user, string role);
        Task <IList<string>> GetRolesAsync(ApplicationUser user);
        Task<bool> PasswordCorrect(ApplicationUser user, string password);
    }
}