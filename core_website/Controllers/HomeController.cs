using System.Diagnostics;
using core_website.Areas.Api.Services;

// KhoaTr - 5/10/2025: Sửa lại namespace từ core_w2 thành core_website
using core_website.Models;
using core_website.Services;
using Microsoft.AspNetCore.Mvc;

namespace core_website.Controllers
// KhoaTr - END
{
  public class HomeController : Controller
  {
    private readonly ILogger<HomeController> _logger;
    private readonly IConfiguration _configuration;
    private readonly ISanPhamService _sanPhamService;

    public HomeController(ILogger<HomeController> logger, IConfiguration configuration, ISanPhamService sanPhamService)
    {
      _logger = logger;
      _configuration = configuration;
      _sanPhamService = sanPhamService;

    }
    public IActionResult Index()
    {
      ViewData["CurrentPage"] = "Home";
      Console.WriteLine("run");
      return View();
    }
    public IActionResult Product()
    {
      ViewData["CurrentPage"] = "Products";
      var products = _sanPhamService.GetAll();
      return View(products);
    }
    public IActionResult Typo()
    {
      ViewData["CurrentPage"] = "Typo";
      return View();
    }
    public IActionResult Contact()
    {
      ViewData["CurrentPage"] = "Contact";
      return View();
    }

    public IActionResult Privacy()
    {
      return View();
    }
    public IActionResult Checkout()
    {
      return View();
    }
    // View Components
    public IActionResult GetProductList()
    {
      var products = _sanPhamService.GetAll();
      return ViewComponent("ProductList", products);
    }
  }
}
