using core_website.Areas.Admins.Models;
using core_website.Areas.Api.Models;
using core_website.Areas.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace core_website.Areas.Admins.Controllers
{
    public class AuthController : Controller
    {
        //Huy - 10/10/25
        private readonly ILogger<AuthController> _logger;
        private readonly INguoiDungService _nguoiDungService;

        public AuthController(ILogger<AuthController> logger, 
            INguoiDungService nguoiDungService
            )
        {
            _logger = logger;
            _nguoiDungService = nguoiDungService;
        }

        [Area("Admins")]
        [HttpGet]// Tin : cập nhật action Login 
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }

        [Area("Admins")]
        [HttpPost]
        public async Task<IActionResult> Login([FromForm] LoginViewModel login)
        {
            //log kiểm tra thông tin trong form
            _logger.LogInformation("Tên đăng nhập: {TenND}", login.TenND);
            _logger.LogInformation("Mật khẩu: {MatKhau}", login.MatKhau);

            //validate
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("ModelState không hợp lệ");
                return View("Login", login);
            }
            //Kiểm tra đăng nhập
            var loginRequest = new NguoiDungLoginRequest()
            {
                TenND = login.TenND,
                MatKhau = login.MatKhau,
            };
            var loginResult = _nguoiDungService.Login(loginRequest);
            if (!loginResult.Success)
            {
              ModelState.AddModelError(string.Empty, "Tên đăng nhập hoặc mật khẩu không đúng.");
              _logger.LogWarning("Đăng nhập thất bại: Tên đăng nhập hoặc mật khẩu không đúng.");
              return View("Login", login);
            } 
            return View("~/Views/Home/Index.cshtml", loginResult.User);
        }
    }
}
