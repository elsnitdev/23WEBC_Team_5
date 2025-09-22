using core_w1.Services;
using Microsoft.AspNetCore.Mvc;

namespace core_w1.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            var users = _userService.GetAllUsers();
            return Json(users);
        }
    }
}
