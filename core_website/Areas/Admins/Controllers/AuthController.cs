using core_website.Areas.Admins.Models;
using core_website.Areas.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace core_website.Areas.Admins.Controllers
{
    public class AuthController : Controller
    {
        //Huy - 10/10/25
        private readonly ILogger<AuthController> _logger;
        private readonly IConfiguration _configuration;
        //private readonly INguoiDungService _nguoiDungService;

        public AuthController(ILogger<AuthController> logger, 
            IConfiguration configuration
            //,INguoiDungService nguoiDungService
            )
        {
            _logger = logger;
            _configuration = configuration;
            //_nguoiDungService = nguoiDungService;
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
            _logger.LogInformation("Tên đăng nhập: ", login.TenDN);
            _logger.LogInformation("Mật khẩu: ", login.MatKhau);

            //validate
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("ModelState không hợp lệ");
                return View("Login", login);
            }
            //Kiểm tra đăng nhập
            if(login.TenDN != "admin" ||  login.MatKhau != "admin")
            {
                return Unauthorized();
            }

            return View("~/Views/Home/Index.cshtml", login);
        }
    }
}
