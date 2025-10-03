using Microsoft.AspNetCore.Authorization;
using FinancialManagement.Interfaces;
using Microsoft.AspNetCore.Mvc;
using FinancialManagement.ViewModels;
using System.Security.Claims;
using FinancialManagement.Data;

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
            // Получаем id текущего пользователя из ClaimsPrincipal
            string currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            ApplicationUser currentUser = await _userRepository.GetByIdAsync(currentUserId);
            List<ApplicationUser> allUsers = await _userRepository.GetAllAsync();
            // Исключаем текущего пользователя из списка
            List<ApplicationUser> otherUsers = allUsers.Where(u => u.Id != currentUser.Id).ToList();
            // Формируем модель для view
            List<UserViewModel> userViewModels = new List<UserViewModel>();

            foreach (var user in allUsers)
            {
                var roles = await _userRepository.GetRolesAsync(user);
                var role = roles.FirstOrDefault() ?? "NoRole";
                userViewModels.Add(new UserViewModel
                {
                    Id = user.UniqueId,
                    Name = user.Name,
                    Email = user.Email!,
                    Tariff = user.Tariff,
                    Role = role,
                });
            }
            return View(userViewModels);
        }
    }
}