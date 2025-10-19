// KhoaTr - 5/10/2025: Sửa lại model + namespace + logic xử lý
using Microsoft.AspNetCore.Mvc;
using core_website.Models;
using core_website.Areas.Admins.Services;
using core_website.Areas.Api.Services;
using core_website.Areas.Api.Models;
using core_website.Areas.Admins.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace core_website.Areas.Admins.Controllers
{
  [Area("Admins")]
    public class ProductsController : Controller
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IDanhMucService _danhMucService;
        // inject Logger + Service + Hosting Environment
        public ProductsController(
            ILogger<ProductsController> logger,
            IDanhMucService danhMucService
        )
        {
            _logger = logger;
            _danhMucService = danhMucService;
    }

        // GET: Admins/Products/Add
        [HttpGet]
        public IActionResult Add()
        {
            var newProduct = new SanPhamFormViewModel();
            newProduct.DanhMucList = _danhMucService.GetAll().Select(d => new SelectListItem
            {
              Value = d.MaDM.ToString(),
              Text = d.TenDM
            })
            .ToList();
            return View(newProduct);
        }
    }
}
// KhoaTr - END