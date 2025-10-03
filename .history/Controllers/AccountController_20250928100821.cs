using FinancialManagement.Interfaces;
using FinancialManagement.ViewModels;
using Microsoft.AspNetCore.Identity;
using FinancialManagement.Data;
using Microsoft.AspNetCore.Mvc;

namespace FinancialManagement.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IRecaptchaService _recaptchaService;
        public AccountController(
            IUserRepository userRepository,
            SignInManager<ApplicationUser> signInManager,
            IRecaptchaService recaptchaService
            )
        {
            _userRepository = userRepository;
            _signInManager = signInManager;
            _recaptchaService = recaptchaService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        // На действие контроллера, которое принимает POST-запрос, нужно добавить атрибут ValidateAntiForgeryToken
        // Атрибут [ValidateAntiForgeryToken] проверит, что токен в форме совпадает с токеном из cookie.
        // Если проверка не пройдена — запрос будет отклонён с ошибкой.
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
                TempData["SuccessMessage"] = "The user has been successfully registered!";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // ModelState.AddModelError("", "Failed to create user");
                TempData["Error"] = "Failed to create user";
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, [FromForm(Name = "g-recaptcha-response")] string recaptchaResponse, string returnUrl = null!)
        {
            // if (!await _recaptchaService.VerifyAsync(recaptchaResponse))
            // {
            //     ModelState.AddModelError(string.Empty, "Пожалуйста, подтвердите, что вы не робот.");
            //     ViewData["ReturnUrl"] = returnUrl;
            //     return View(model);
            // }

            var user = await _userRepository.GetByEmailAsync(model.Email);
            if (user == null)
            {
                // ModelState.AddModelError("Email", "The user with this email is not registered.");
                TempData["Error"] = "The user with this email is not registered.";
            }
            else
            {
                var passwordCorrect = await _userRepository.PasswordCorrect(user, model.Password);

                if (!passwordCorrect)
                {
                    // ModelState.AddModelError("Password", "Invalid password");
                    TempData["Error"] = "Invalid password";
                }
                else
                {
                    ViewData["ReturnUrl"] = returnUrl;
                    var result = await _signInManager.PasswordSignInAsync(
                        model.Email,
                        model.Password,
                        model.RememberMe,
                        lockoutOnFailure: false
                    );
                    if (result.Succeeded)
                    {
                        if (Url.IsLocalUrl(returnUrl))
                        {
                            return Redirect(returnUrl);
                        }
                        else
                        {
                            TempData["Success"] = "You have successfully logged into your account.";
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                    {
                        // ModelState.AddModelError(string.Empty, "Login error");
                        TempData["Error"] = "Login error";
                        return View(model);
                    }
                }
            }
            // Если мы дошли сюда — значит user == null или password неверен — возвращаем View с ошибками// Если мы дошли сюда — значит user == null
            // или password неверен — возвращаем View с ошибками
            ViewData["ReturnUrl"] = returnUrl;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        // ValidateAntiForgeryToken его отключать не рекомендуется. Это стандартная и важная мера защиты, которая способствует безопасности вашего приложения.
        // В случае logout — это особенно важно, потому что злоумышленник не сможет инициировать выход пользователя через поддельный POST-запрос без 
        // правильного токена.
        // Он должен оставаться, чтобы обеспечить безопасную защиту от CSRF. В форме для logout обязательно вставляйте @Html.AntiForgeryToken(), как в прошлом примере.
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}