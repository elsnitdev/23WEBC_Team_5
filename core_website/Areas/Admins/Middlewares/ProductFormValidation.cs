// KhoaTr - 5/10/2025: Sửa lại model
using System.ComponentModel.DataAnnotations;
using core_website.Areas.Admins.Services;
using core_website.Models;

namespace core_website.Areas.Admins.Middlewares
{
    public class ProductFormValidation
    {
        private readonly RequestDelegate _next;
        private readonly IImageProcessingService _imageService;
        public ProductFormValidation(RequestDelegate next, IImageProcessingService imageService)
        {
            _next = next;
            _imageService = imageService;
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

                    // KhoaTr - 7/10/2025: Kiểm tra file hình ảnh
                    var imageFiles = httpContext.Request.Form.Files.GetFiles("HinhAnh");
                    if (imageFiles != null && imageFiles.Any())
                    {
                      foreach (var imageFile in imageFiles)
                      {
                        if (imageFile.Length > 0)
                        {
                          var (IsValid, ErrorMessage) = await _imageService.ValidateImageAsync(imageFile);
                          if (!IsValid)
                          {
                            isValid = false;
                            validationResults.Add(new ValidationResult(ErrorMessage, ["HinhAnh"]));
                            await _next(httpContext);
                          }
                        }
                      }
                    }
                    else
                    {
                      isValid = false;
                      validationResults.Add(new ValidationResult("Hình ảnh là bắt buộc", ["HinhAnh"]));
                    }

                    if (!isValid)
                    {
                      var errors = validationResults.Select(vr => new
                      {
                        Key = vr.MemberNames.FirstOrDefault() ?? string.Empty,
                        vr.ErrorMessage
                      }).ToList();
                      httpContext.Items["ValidationErrors"] = errors;
                      httpContext.Items["IsValid"] = false;
                    }
                    else
                    {
                      httpContext.Items["IsValid"] = true;
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
// KhoaTr - END