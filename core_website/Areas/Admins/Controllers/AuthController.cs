using System.Security.Claims;
using core_website.Areas.Admins.Models;
using core_website.Areas.Api.Models;
using core_website.Areas.Api.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace core_website.Areas.Admins.Controllers
{
    [Area("Admins")]
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

        [HttpGet]// Tin : cập nhật action Login 
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }

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
              ModelState.AddModelError(string.Empty, loginResult.Message ?? "Đã có lỗi xảy ra, vui lòng thử lại!");
              _logger.LogWarning($"Đăng nhập thất bại: {loginResult.Message ?? "Đã có lỗi xảy ra, vui lòng thử lại!"}");
              return View("Login", login);
            }
            var claims = new[] { 
              new Claim(ClaimTypes.Name, loginResult.User.TenND),
              new Claim(ClaimTypes.Role, loginResult.User.VaiTro)
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(new ClaimsPrincipal(identity));
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> Logout()
        {
          await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
          return RedirectToAction("Login");
        }
  }
}
