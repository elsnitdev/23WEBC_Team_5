using System.Diagnostics;
using System.Reflection;
using core_website.Areas.Api.Models;
using core_website.Areas.Api.Services;

// KhoaTr - 5/10/2025: Sửa lại namespace từ core_w2 thành core_website
using core_website.Models;
using core_website.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
namespace core_website.Controllers
// KhoaTr - END
{
  public class HomeController : Controller
  {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private readonly ISanPhamService _sanPhamService;
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(
            ILogger<HomeController> logger,
            IConfiguration configuration,
            ISanPhamService sanPhamService,
            IHttpClientFactory httpClientFactory
    )
    {
            _logger = logger;
            _configuration = configuration;
            _sanPhamService = sanPhamService;
            _httpClientFactory = httpClientFactory;

        }
    public IActionResult Index()
    {
      ViewData["CurrentPage"] = "Home";
      Console.WriteLine("run");
      return View();
    }
        // Tin Truy vấn API, chuyển JSON thành model, truyền vào View
        public async Task<IActionResult> Product()
        {
            ViewData["ActivePage"] = "Product";  

            List<SanPham> sanPhams = new List<SanPham>();  
            try
            {
                //Tin truy vấn dữ liệu từ API
                var client = _httpClientFactory.CreateClient("ProductApiClient");  
                var response = await client.GetAsync("api/products");  //Tin Gọi API từ ProductsController

                if (response.IsSuccessStatusCode)
                {
                    //Tin Đọc JSON 
                    var jsonString = await response.Content.ReadAsStringAsync();

                    //Tin chuyển JSON từ API thành model List<SanPham>
                    sanPhams = JsonSerializer.Deserialize<List<SanPham>>(jsonString, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true  
                    });
                }
                else
                {
                    ViewBag.Error = "Lỗi gọi API: " + response.StatusCode;
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Lỗi: " + ex.Message;
            }

           
            return View(sanPhams);  
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
    public IActionResult GetProductList([FromBody] IEnumerable<SanPham> data)
    {
      if (data == null)
      {
        return BadRequest(new { Status = "Error", Message = "Invalid data" });
      }
      return ViewComponent("ProductList", data);
    }
  }
}
