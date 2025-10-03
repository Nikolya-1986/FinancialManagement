using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FinancialManagement.Models;
using FinancialManagement.Interfaces;
using FinancialManagement.ViewModels;

namespace FinancialManagement.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IUserRepository _userRepository;

    public HomeController(ILogger<HomeController> logger, IUserRepository userRepository)
    {
        _logger = logger;
        _userRepository = userRepository;
    }

    public async Task<IActionResult> Index()
    {
        if (User.Identity!.IsAuthenticated)
        {
            var email = User.Identity.Name;
            var user = await _userRepository.GetByEmailAsync(email!);
            if (user != null)
            {
                IList<string> roles = await _userRepository.GetRolesAsync(user);
                string userRole = roles.FirstOrDefault() ?? "Role not assigned!";
                var model = new UserViewModel
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email!,
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
