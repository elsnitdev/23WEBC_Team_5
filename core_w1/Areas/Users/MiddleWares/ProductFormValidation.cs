using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using core_w2.Areas.Users.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace core_w2.Areas.Users.MiddleWares
{
  public class ProductFormValidation
  {
    private readonly RequestDelegate _next;

    public ProductFormValidation(RequestDelegate next)
    {
      _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
      if (httpContext.Request.Method == HttpMethods.Post &&
          httpContext.Request.Path.StartsWithSegments("/Api/Products/Create"))
      {
        try
        {
          var form = await httpContext.Request.ReadFormAsync();
          var sanPham = new SanPham
          {
            TenSP = form["TenSP"],
            DonGia = decimal.TryParse(form["DonGia"], out var donGia) ? donGia : 0,
            DonGiaKhuyenMai = decimal.TryParse(form["DonGiaKhuyenMai"], out var donGiaKM) ? donGiaKM : 0,
            HinhAnh = form["HinhAnh"],
            MoTa = form["MoTa"],
            LoaiSP = form["LoaiSP"]
          };
          var validationResults = new List<ValidationResult>();
          var validationContext = new ValidationContext(sanPham);
          bool isValid = Validator.TryValidateObject(
              sanPham,
              validationContext,
              validationResults,
              true);

          if (!isValid)
          {
            // Tạo response lỗi
            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            httpContext.Response.ContentType = "application/json";

            var errors = validationResults.Select(vr => vr.ErrorMessage);
            var errorResponse = new
            {
              Success = false,
              Errors = errors
            };

            await httpContext.Response.WriteAsJsonAsync(errorResponse);
            return;
          }
        }
        catch (System.Exception ex)
        {
          httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
          httpContext.Response.ContentType = "application/json";
          await httpContext.Response.WriteAsJsonAsync(new
          {
            Success = false,
            Errors = new[] { $"Lỗi xử lý form data: {ex.Message}" }
          });
          return;
        }
        await _next(httpContext);
      }
    }
  }
  public static class ProductFormValidationExtensions
  {
    public static IApplicationBuilder UseProductFormValidation(this IApplicationBuilder builder)
    {
      return builder.UseMiddleware<ProductFormValidation>();
    }
  }
}
