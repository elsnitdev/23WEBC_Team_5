using Microsoft.AspNetCore.Mvc;

namespace core_website.Areas.Admins.Controllers
{
    public class AuthController : Controller
    {
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
    }
}
