using System.Diagnostics;
using core_w2.Areas.Users.Models;
using core_w2.Areas.Users.Services;
using Microsoft.AspNetCore.Mvc;

namespace core_w2.Areas.Users.Controllers
{
    [Area("Users")]
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
                Console.WriteLine("run");
            return View();
    }
    public IActionResult Product()
    {
      var products = _sanPhamService.GetAll();
      return View(products);
    }

    public IActionResult Privacy()
    {
      return View();
    }
        public IActionResult Checkout()
        {
         
        var listProducts =    _sanPhamService.GetAll();
            List<SanPham> listCheckout = [_sanPhamService.GetById(3), _sanPhamService.GetById(6), _sanPhamService.GetById(2)];
            Console.WriteLine(listProducts.Count.ToString());
            return View(listCheckout);
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
