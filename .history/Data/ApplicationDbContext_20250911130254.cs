using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FinancialManagement.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        #pragma warning disable CS0114 // Member hides inherited member; missing override keyword
        public DbSet<ApplicationUser> Users { get; set; }
        #pragma warning restore CS0114 // Member hides inherited member; missing override keyword
    }
}