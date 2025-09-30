using System.Diagnostics;
using core_w2.Models;
using core_w2.Services;
using Microsoft.AspNetCore.Mvc;

namespace core_w2.Controllers
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
    public IActionResult TestAppSettings()
    {
      long maxFileSize = _configuration.GetValue<long>("AppSettings:MaxFileSize");
      Console.WriteLine("Max file size: " + maxFileSize);
      string[] listBannedIPs = _configuration.GetSection("AppSettings:ListBannedIPs").Get<string[]>();
      Console.WriteLine("List banned IPs: " + string.Join(", ", listBannedIPs));
      return View("Index");
    }

    public IActionResult TestCustomAppSettings()
    {
      long maxFileSize = _configuration.GetValue<long>("CustomAppSettings:MaxFileSize");
      Console.WriteLine("Max file size (file customappsettings): " + maxFileSize);
      string[] listBannedIPs = _configuration.GetSection("CustomAppSettings:ListBannedIPs").Get<string[]>();
      Console.WriteLine("List banned IPs (file customappsettings): " + string.Join(", ", listBannedIPs));
      return View("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
  }
}
