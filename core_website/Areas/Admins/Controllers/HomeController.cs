using core_website.Areas.Api.Models;
using core_website.Areas.Api.Services;
using Microsoft.AspNetCore.Mvc;

// KhoaTr - 5/10/2025: Sửa lại namespace từ core_w2 thành core_website
namespace core_website.Areas.Admins.Controllers
// KhoaTr - END
{
  [Area("Admins")]
    public class HomeController : Controller
    {
        private readonly ISanPhamService _sanPhamService;
        public HomeController(ISanPhamService sanPhamService)
        {
          _sanPhamService = sanPhamService;
        }
      
        public IActionResult Blank()
        {
            return View();
        }
        public IActionResult Index()
        {
            List<SanPham> dsSanPham = _sanPhamService.GetAll().OrderBy(sp => sp.MaSP).ToList();
            return View(dsSanPham);
        }
  }
}
