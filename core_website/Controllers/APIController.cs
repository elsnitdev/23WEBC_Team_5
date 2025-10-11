using Microsoft.AspNetCore.Mvc;
using System.Text.Json;  // Để deserialize JSON
using core_website.Areas.Api.Models;  // Namespace của model SanPham (khớp JSON từ API)

namespace core_website.Controllers
{
    public class APIController : Controller  
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public APIController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;  // Inject factory dùng để tạo HttpClient
        }

        //Tin Lấy danh sách tất cả sản phẩm từ API
        public async Task<IActionResult> Index()
        {
            List<SanPham> sanPhams = new List<SanPham>(); 
            try
            {
                var client = _httpClientFactory.CreateClient("ProductApiClient"); 
                var response = await client.GetAsync("api/products");  
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    // Deserialize JSON từ API thành model List<SanPham>
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

        //Tin Lấy chi tiết sản phẩm theo ID từ API
        public async Task<IActionResult> Details(int id)  
        {
            try
            {
                var client = _httpClientFactory.CreateClient("ProductApiClient");

             
                var response = await client.GetAsync($"api/products/{id}");

                if (!response.IsSuccessStatusCode)
                {
                    ViewBag.Error = "Không tìm thấy hoặc lỗi API";
                    return View(new SanPham());
                }

                var jsonString = await response.Content.ReadAsStringAsync();

             
                var sanPham = JsonSerializer.Deserialize<SanPham>(jsonString, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return View(sanPham);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Lỗi: " + ex.Message;
                return View(new SanPham());
            }
        }

        // Tin Tìm kiếm theo keyword 
        public async Task<IActionResult> Search(string keyword)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("ProductApiClient");

              
                var response = await client.GetAsync($"api/products/search?keyword={keyword}");

                if (!response.IsSuccessStatusCode)
                {
                    ViewBag.Error = "Lỗi tìm kiếm API";
                    return View(new List<SanPham>());
                }

                var jsonString = await response.Content.ReadAsStringAsync();
                var sanPhams = JsonSerializer.Deserialize<List<SanPham>>(jsonString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                return View(sanPhams);  
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Lỗi: " + ex.Message;
                return View(new List<SanPham>());
            }
        }

        // Tin Lấy sản phẩm khuyến mãi
       
        public async Task<IActionResult> KhuyenMai()
        {
            try
            {
                var client = _httpClientFactory.CreateClient("ProductApiClient");

               
                var response = await client.GetAsync("api/products/khuyenmai");

                if (!response.IsSuccessStatusCode)
                {
                    ViewBag.Error = "Lỗi API";
                    return View(new List<SanPham>());
                }

                var jsonString = await response.Content.ReadAsStringAsync();
                var sanPhams = JsonSerializer.Deserialize<List<SanPham>>(jsonString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                return View(sanPhams);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Lỗi: " + ex.Message;
                return View(new List<SanPham>());
            }
        }
    }
}