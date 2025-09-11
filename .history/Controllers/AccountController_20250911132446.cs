using FinancialManagement.Data;
using FinancialManagement.Interfaces;
using FinancialManagement.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FinancialManagement.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(IUserRepository userRepository, SignInManager<ApplicationUser> signInManager)
        {
            _userRepository = userRepository;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userExists = await _userRepository.GetByEmailAsync(model.Email);
            if (userExists != null)
            {
                ModelState.AddModelError(string.Empty, "A user with this email already exists.");
                return View(model);
            }

            var user = new ApplicationUser
            {
                UniqueId = Guid.NewGuid(),
                UserName = model.Email,
                Email = model.Email,
                Name = model.Name,
                Tariff = Tariff.Simple,
            };

            bool isSuccess = await _userRepository.CreateUserAsync(user, model.Password);

            if (isSuccess)
            {
                await _userRepository.AddToRoleAsync(user, Role.User.ToString());
                // Автоматический вход после регистрации
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Failed to create user");
                return View(model);
            }
        }
    }
}