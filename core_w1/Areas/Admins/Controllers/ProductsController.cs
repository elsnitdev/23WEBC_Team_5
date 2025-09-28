using System.Reflection;
using core_w2.Areas.Users.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace core_w2.Areas.Admins.Controllers
{
  [Area("Admins")]
  public class ProductsController : Controller
  {
    private readonly ILogger<ProductsController> _logger;

    public ProductsController(ILogger<ProductsController> logger)
    {
      _logger = logger;
    }
    // GET: Admins/Products/Add
    [HttpGet]
    public IActionResult Add()
    {
      return View(new SanPham());
    }

    // POST: Api/Products/Create
    [HttpPost]
    [Route("Api/Products/Create")]
    public async Task<IActionResult> Create([FromForm] SanPham sanPham, IFormFile? hinhAnh)
    {
      _logger.LogInformation("Nhận POST request /Api/Products/Create");

      if (!ModelState.IsValid)
      {
        return View("Add", sanPham);
      }

      TempData["SuccessMessage"] = "Tạo sản phẩm thành công";
      return RedirectToAction("Add");
    }
  }
}
