using Microsoft.AspNetCore.Authorization;
using FinancialManagement.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Index()
        {
            return View();
        }
    }
}