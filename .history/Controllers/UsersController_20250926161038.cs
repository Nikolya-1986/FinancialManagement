using Microsoft.AspNetCore.Authorization;
using FinancialManagement.ViewModels;
using FinancialManagement.Interfaces;
using Microsoft.AspNetCore.Mvc;
using FinancialManagement.Data;
using System.Security.Claims;
using System.Threading.Tasks;

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUser(string id)
        {
            Console.WriteLine($"Received id: {id}");
            if (string.IsNullOrEmpty(id))
            {
                TempData["Error"] = "Не передан идентификатор пользователя.";
                return RedirectToAction("Index");
            }

            bool success = await _userRepository.DeleteUserById(id);

            if (success)
            {
                TempData["Message"] = "Пользователь успешно удален.";
            }
            else
            {
                TempData["Error"] = "Не удалось удалить пользователя.";
            }

            return RedirectToAction("Index");
        }       
    }
}