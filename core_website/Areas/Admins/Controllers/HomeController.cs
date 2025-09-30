using Microsoft.AspNetCore.Mvc;

namespace core_w2.Areas.Admins.Controllers
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
