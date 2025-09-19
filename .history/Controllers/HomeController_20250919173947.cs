using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FinancialManagement.Models;
using FinancialManagement.Interfaces;
using Microsoft.AspNetCore.Identity;
using FinancialManagement.Data;
using FinancialManagement.ViewModels;

namespace FinancialManagement.Controllers;

public class HomeController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ILogger<HomeController> _logger;
    private readonly IUserRepository _userRepository;

    public HomeController(ILogger<HomeController> logger, IUserRepository userRepository, UserManager<ApplicationUser> userManager)
    {
        _logger = logger;
        _userManager = userManager;
        _userRepository = userRepository;
    }

    public async Task<IActionResult> Index()
    {
        if (User.Identity.IsAuthenticated)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user != null)
            {
                IList<string> roles = await _userManager.GetRolesAsync(user);
                string userRole = roles.FirstOrDefault() ?? "Роль не назначена";
                var model = new UserViewModel
                {
                    Id = user.UniqueId,
                    Name = user.Name,
                    Email = user.Email,
                    Tariff = user.Tariff,
                    Role = userRole
                };
                return View(model);
            }
        }
        return View();
    }

    public IActionResult Privacy()
    {

        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
