using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using core_website.Models;
using core_website.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace core_website.Areas.Admins.Middlewares
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
            if (
              httpContext.Request.Method == HttpMethods.Post &&
              httpContext.Request.Path.StartsWithSegments("/Api/Products/Create"))
            {
                try
                {
                    var form = await httpContext.Request.ReadFormAsync();
                    var sanPham = new SanPhamViewModel
                    {
                        TenSP = form["TenSP"],
                        DonGia = decimal.TryParse(form["DonGia"], out var donGia) ? donGia : 0,
                        KhuyenMai = decimal.TryParse(form["KhuyenMai"], out var donGiaKM) ? donGiaKM : 0,
                        HinhAnh = form["HinhAnh"],
                        MoTa = form["MoTa"],
                        Tag = form["Tag"]
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
                        var errors = validationResults.Select(vr => new
                        {
                            Key = vr.MemberNames.FirstOrDefault() ?? string.Empty,
                        }).ToList();
                        httpContext.Items["ValidationErrors"] = errors;
                        httpContext.Items["IsValid"] = false;
                    }
                }
                catch (Exception ex)
                {
                    httpContext.Items["ValidationErrors"] = new[]
                                {
                            new { Key = string.Empty, ErrorMessage = $"Lỗi xử lý form data: {ex.Message}" }
                        }.ToList();
                    httpContext.Items["IsValid"] = false;
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