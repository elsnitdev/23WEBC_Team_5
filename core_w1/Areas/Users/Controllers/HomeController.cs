using System.Diagnostics;
using core_w2.Areas.Users.Models;
using Microsoft.AspNetCore.Mvc;

namespace core_w2.Areas.Users.Controllers
{
    [Area("Users")]
    public class HomeController : Controller
  {
    private readonly ILogger<HomeController> _logger;
    private readonly IConfiguration _configuration;

    public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
    {
      _logger = logger;
      _configuration = configuration;

    }
    public IActionResult Index()
    {
      return View();
    }

    public IActionResult Privacy()
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
