using FinancialManagement.Data;

namespace FinancialManagement.Interfaces
{
    public interface IUserRepository
    {
        Task<ApplicationUser> GetByIdAsync(string id);
        Task<ApplicationUser> GetByEmailAsync(string email);
        Task<IEnumerable<ApplicationUser>> GetAllAsync();
        Task AddUserAsync(ApplicationUser user);
        Task DeleteUserAsync(ApplicationUser user);
    }
}