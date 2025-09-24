using core_w2.Areas.Users.Models;
using core_w2.Areas.Users.Services;
using Microsoft.AspNetCore.Mvc;

namespace core_w2.Areas.Users.Controllers
{
    [Area("Users")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _config;
    // KhoaTr - 22/09/2025: Inject IUserService vào UserController thông qua constructor

    public UserController(IUserService userService, IConfiguration config)
        {
            _userService = userService;
          _config = config;
        }
        // KhoaTr - 22/09/2025: Chỉnh sửa action Index lấy danh sách người dùng từ UserService và truyền vào ViewData để hiển thị trong View
        public IActionResult Index(int page = 1)
        {
            Console.WriteLine($"_config: {_config}");
            int itemsPerPage = _config.GetValue<int>("AppSettings:Common:ItemsPerPage");
            var queryData = _userService.GetAllUsers((page-1) * itemsPerPage, itemsPerPage);
            ViewData["Users"] = queryData.Users as List<User>;
            ViewData["CurrentPage"] = page;
            ViewData["TotalPages"] = queryData.Total;
            ViewData["ItemsPerPage"] = itemsPerPage;
            ViewData["TotalUsers"] = queryData.Total;

            return View();
        }
    }
}
