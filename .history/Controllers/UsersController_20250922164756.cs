using Microsoft.AspNetCore.Authorization;
using FinancialManagement.Interfaces;
using Microsoft.AspNetCore.Mvc;
using FinancialManagement.ViewModels;

namespace FinancialManagement.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var users = await _userRepository.GetAllAsync();
            var usersWithRoles = new List<UserViewModel>();

            foreach (var user in users)
            {
                var roles = await _userRepository.GetRolesAsync(user);
                var role = roles.FirstOrDefault() ?? "NoRole";
                usersWithRoles.Add(new UserViewModel
                {
                    Id = user.UniqueId,
                    Name = user.Name,
                    Email = user.Email!,
                    Tariff = user.Tariff,
                    Role = role,
                });
            }
            return View(users);
        }
    }
}