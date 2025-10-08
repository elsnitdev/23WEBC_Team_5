    using Microsoft.AspNetCore.Mvc;

// KhoaTr - 5/10/2025: Sửa lại namespace từ core_w2 thành core_website
namespace core_website.Areas.Admins.Controllers
// KhoaTr - END
{
  [Area("Admins")]
    public class HomeController : Controller
    {
      
        public IActionResult Blank()
        {
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }
  }
}
