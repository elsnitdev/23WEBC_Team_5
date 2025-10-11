// KhoaTr - 5/10/2025: Sửa lại model + namespace + logic xử lý
using Microsoft.AspNetCore.Mvc;
using core_website.Models;
using core_website.Areas.Admins.Services;
using core_website.Areas.Api.Services;
using core_website.Areas.Api.Models;

namespace core_website.Areas.Admins.Controllers
{
  [Area("Admins")]
    public class ProductsController : Controller
    {
        private readonly ILogger<ProductsController> _logger;
        // inject Logger + Service + Hosting Environment
        public ProductsController(
            ILogger<ProductsController> logger
        )
        {
            _logger = logger;
        }

        // GET: Admins/Products/Add
        [HttpGet]
        public IActionResult Add()
        {
            return View(new SanPhamViewModel());
        }
    }
}
// KhoaTr - END