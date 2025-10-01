using FinancialManagement.Data;

namespace FinancialManagement.Interfaces
{
    public interface IUserRepository
    {
        Task<List<ApplicationUser>> GetAllAsync();
        Task<ApplicationUser> GetByIdAsync(string id);
        Task<ApplicationUser> GetByEmailAsync(string email);
        Task<bool> UpdateUserAsync(ApplicationUser user);
        Task<bool> CreateUserAsync(ApplicationUser user, string password);
        Task<bool> DeleteUserById(string id);
        Task AddToRoleAsync(ApplicationUser user, string role);
        Task<bool> UpdateUserRoleAsync(ApplicationUser user, string newRole);
        Task<IList<string>> GetRolesAsync(ApplicationUser user);
        Task<bool> PasswordCorrect(ApplicationUser user, string password);
    }
}