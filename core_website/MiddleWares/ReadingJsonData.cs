using core_website.Models;
using core_website.Services;
using System.Text.Json;

namespace core_website.MiddleWares
{
  public class ReadingJsonData
  {
    private readonly RequestDelegate _next;

    public ReadingJsonData(RequestDelegate next)
    {
      _next = next;
    }

    public async Task InvokeAsync(HttpContext context, ISanPhamService sanPhamService, IWebHostEnvironment env)
    {
      var filePath = Path.Combine(env.ContentRootPath, "db.json");
      Console.WriteLine("path: " + filePath);

      if (File.Exists(filePath))
      {
        var jsonString = File.ReadAllText(filePath);
        Console.WriteLine("Json string: " + jsonString);

        var jsonData = JsonSerializer.Deserialize<JsonData>(jsonString);
        Console.WriteLine("Json data: " + (jsonData != null ? jsonData.ToString() : "null"));

        if (jsonData?.Products != null)
        {
          // Cập nhật danh sách sản phẩm trong SanPhamService
          sanPhamService.UpdateList(jsonData.Products);

          // In danh sách sản phẩm để kiểm tra
          foreach (var p in jsonData.Products)
          {
            Console.WriteLine(p.TenSP);
          }
        }
        else
        {
          Console.WriteLine("Lỗi: jsonData hoặc jsonData.Products là null.");
        }
      }

      // Chuyển tiếp request đến middleware tiếp theo
      await _next(context);
    }

    // Class để deserialize JSON
    private class JsonData
    {
      public List<SanPham> Products { get; set; }
      public int Total { get; set; }
    }
  }
}
