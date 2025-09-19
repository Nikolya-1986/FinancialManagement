using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FinancialManagement.Models;
using FinancialManagement.Interfaces;
using Microsoft.AspNetCore.Identity;
using FinancialManagement.Data;

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
                return View(user);
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
