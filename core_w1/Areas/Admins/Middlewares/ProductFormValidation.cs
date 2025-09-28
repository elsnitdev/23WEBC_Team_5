using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using core_w2.Areas.Users.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace core_w2.Areas.Admins.Middlewares
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
            var isAjaxRequest = httpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest";

            if (isAjaxRequest)
            {
              // Return JSON response for AJAX/API calls
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
            else
            {
              // Add errors to ModelState for non-AJAX requests
              var errors = validationResults.Select(vr => new
              {
                Key = vr.MemberNames.FirstOrDefault() ?? string.Empty,
                ErrorMessage = vr.ErrorMessage
              }).ToList();
              httpContext.Items["ValidationErrors"] = errors;
              httpContext.Items["IsValid"] = false;
            }
          }
        }
        catch (Exception ex)
        {
          var isAjaxRequest = httpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest";

          if (isAjaxRequest)
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
          else
          {
            httpContext.Items["ValidationErrors"] = new[]
                        {
                            new { Key = string.Empty, ErrorMessage = $"Lỗi xử lý form data: {ex.Message}" }
                        }.ToList();
            httpContext.Items["IsValid"] = false;
          }
        }
      }

      await _next(httpContext);
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
