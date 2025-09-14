using FinancialManagement.Data;
using FinancialManagement.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FinancialManagement.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<ApplicationUser> signInManager
            )
        {
            _userManager = userManager;
            _roleManager = roleManager;
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

            var userExists = await _userManager.FindByEmailAsync(model.Email);
            if (userExists != null)
            {
                ModelState.AddModelError(string.Empty, "Пользователь с таким Email уже существует.");
                return View(model);
            }

            var user = new ApplicationUser
            {
                UniqueId = Guid.NewGuid(),
                Email = model.Email,
                Name = model.Name,
                Tariff = Tariff.Simple,
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                // Создаем роль User, если не существует
                if (!await _roleManager.RoleExistsAsync(Role.User.ToString()))
                {
                    await _roleManager.CreateAsync(new IdentityRole(Role.User.ToString()));
                }

                // Назначаем роль User
                await _userManager.AddToRoleAsync(user, Role.User.ToString());
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "Home");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(model);
        }
    }
}