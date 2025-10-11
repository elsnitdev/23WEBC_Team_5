using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using core_website.Areas.Api.Models;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using core_website.Areas.Api.Services;
using core_website.Areas.Admins.Models;
using NuGet.Protocol.Plugins;


// KhoaTr - 5/10/2025: Sửa lại namespace từ core_w2 thành core_website
namespace core_website.Areas.Admins.Controllers
// KhoaTr - END
{
  [Area("Admins")]

    public class HomeController : Controller
    {
        private readonly ILogger<AuthController> _logger;
        private readonly INguoiDungService _nguoiDungService;

        public HomeController(ILogger<AuthController> logger,
            INguoiDungService nguoiDungService
            )
        {
            _logger = logger;
            _nguoiDungService = nguoiDungService;
        }

       
        public IActionResult Blank([FromForm] LoginViewModel login)
        {
            //log kiểm tra thông tin trong form
            _logger.LogInformation("Tên đăng nhập: {TenND}", login.TenND);
            _logger.LogInformation("Mật khẩu: {MatKhau}", login.MatKhau);

            //validate
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("ModelState không hợp lệ");
                return View("~/Areas/Admins/Views/Auth/Login.cshtml");
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
                return View("~/Areas/Admins/Views/Auth/Login.cshtml");
            }
            return View();
        }
        
        public IActionResult Index([FromForm] LoginViewModel login)
        {
            //log kiểm tra thông tin trong form
            _logger.LogInformation("Tên đăng nhập: {TenND}", login.TenND);
            _logger.LogInformation("Mật khẩu: {MatKhau}", login.MatKhau);

            //validate
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("ModelState không hợp lệ");
                return View("~/Areas/Admins/Views/Auth/Login.cshtml");
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
                return View("~/Areas/Admins/Views/Auth/Login.cshtml");
            }
            return View();
        }
  }
}
