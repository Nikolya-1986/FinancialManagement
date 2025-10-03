using Microsoft.AspNetCore.Authorization;
using FinancialManagement.ViewModels;
using FinancialManagement.Interfaces;
using Microsoft.AspNetCore.Mvc;
using FinancialManagement.Data;
using System.Security.Claims;

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

            foreach (var user in otherUsers)
            {
                var roles = await _userRepository.GetRolesAsync(user);
                var role = roles.FirstOrDefault() ?? "NoRole";
                userViewModels.Add(new UserViewModel
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email!,
                    Tariff = user.Tariff,
                    Role = role,
                    IsBlocked = user.IsBlocked,
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
                TempData["Error"] = "User ID not passed.";
                return RedirectToAction("Index");
            }

            bool success = await _userRepository.DeleteUserById(id);
            if (success)
            {
                TempData["Success"] = "The user was successfully deleted.";
            }
            else
            {
                TempData["Error"] = $"Failed to delete user. User with id {id} not found.";
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleBlockUser(string id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
            {
                TempData["Error"] = "User not found.";
                return RedirectToAction("Index");
            }

            user.IsBlocked = !user.IsBlocked;
            bool updateResult = await _userRepository.UpdateUserAsync(user);

            if (!updateResult)
            {
                TempData["Error"] = "Ошибка при обновлении пользователя.";
            }
            else
            {
                TempData["Success"] = $"Пользователь {(user.IsBlocked ? "заблокирован" : "разблокирован")}.";
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUserRole(string userId, List<string> newRoles)
        {
            if (string.IsNullOrEmpty(userId) || newRoles == null)
            {
                TempData["Error"] = "User ID or Roles not specified";
                return RedirectToAction("Index");
            }

            var user = await _userRepository.GetByIdAsync(userId);

            if (user == null)
            {
                TempData["Error"] = "User not found.";
                return RedirectToAction("Index");
            }

            bool updated = await _userRepository.UpdateUserRoleAsync(user, newRoles);

            if (updated)
            {
                TempData["Success"] = "User role updated";
            }
            else
            {
                TempData["Error"] = "Error updating role";
            }

            return RedirectToAction("Index");
        }
    }
}