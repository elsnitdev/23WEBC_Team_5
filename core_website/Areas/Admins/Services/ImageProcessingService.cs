using System.Drawing;
using System.Drawing.Imaging;

namespace core_website.Areas.Admins.Services
{
  public class ImageProcessingService : IImageProcessingService
  {
    public async Task<(bool IsValid, string ErrorMessage)> ValidateImageAsync(IFormFile file, long maxSizeBytes = 5 * 1024 * 1024)
    {
      // Kiểm tra nếu file null hoặc rỗng
      if (file == null || file.Length == 0)
        return (false, "No file uploaded");

      // Kiểm tra kích thước file
      if (file.Length > maxSizeBytes)
        return (false, $"File size exceeds the limit of {maxSizeBytes / (1024 * 1024)} MB");

      // Kiểm tra định dạng file
      var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };
      var extension = Path.GetExtension(file.FileName).ToLower();
      if (!allowedExtensions.Contains(extension))
        return (false, "Unsupported file format");

      // Kiểm tra nếu file có phải hình ảnh không
      try
      {
        // Sử dụng System.Drawing để kiểm tra tính hợp lệ của hình ảnh
        // Hiện tại chỉ hỗ trợ windows
        // Nếu cần hỗ trợ đa nền tảng, có thể cài thêm thư viện khác như ImageSharp
        using var image = Image.FromStream(file.OpenReadStream());
        return (true, string.Empty);
      }
      catch (Exception)
      {
        return (false, "File is not a valid image");
      }
    }
    public async Task<string> ProcessAndSaveImageAsync(IFormFile file, string destinationPath, string? fileName)
    {
      // Validate file trước khi lưu
      var (isValid, errorMessage) = await ValidateImageAsync(file);
      if (!isValid)
        throw new InvalidOperationException(errorMessage);

      // Tạo thư mục nếu chưa tồn tại
      if (!Directory.Exists(destinationPath))
      {
        Directory.CreateDirectory(destinationPath);
      }
      // Tạo tên file duy nhất để tránh ghi đè
      var uniqueFileName = $"{(fileName != null ? fileName : Guid.NewGuid())}{Path.GetExtension(file.FileName)}";
      var filePath = Path.Combine(destinationPath, uniqueFileName);
      // Lưu file vào đường dẫn đã chỉ định
      using (var stream = new FileStream(filePath, FileMode.Create))
      {
        await file.CopyToAsync(stream);
      }
      return filePath;
    }
    public async Task<Stream> ResizeImageAsync(IFormFile file, int maxWidth, int maxHeight, bool preserveAspectRatio = true)
    {
      // Validate file trước khi xử lý
      var (isValid, errorMessage) = await ValidateImageAsync(file);
      if (!isValid)
        throw new InvalidOperationException(errorMessage);

      // Hiện tại chỉ hỗ trợ windows
      // Nếu cần hỗ trợ đa nền tảng, có thể cài thêm thư viện khác như ImageSharp
      using var image = Image.FromStream(file.OpenReadStream());

      int targetWidth = maxWidth;
      int targetHeight = maxHeight;

      // Tính toán kích thước mới nếu cần giữ tỉ lệ hình
      if (preserveAspectRatio)
      {
        var ratioX = (double)maxWidth / image.Width;
        var ratioY = (double)maxHeight / image.Height;
        var ratio = Math.Min(ratioX, ratioY);
        targetWidth = (int)(image.Width * ratio);
        targetHeight = (int)(image.Height * ratio);
      }

      // Thực hiện resize
      // Hiện tại chỉ hỗ trợ windows
      // Nếu cần hỗ trợ đa nền tảng, có thể cài thêm thư viện khác
      using var resizedImage = new Bitmap(targetWidth, targetHeight);
      using (var graphics = Graphics.FromImage(resizedImage))
      {
        graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
        graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
        graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
        graphics.DrawImage(image, 0, 0, targetWidth, targetHeight);
      }

      var outputStream = new MemoryStream();
      resizedImage.Save(outputStream, image.RawFormat);
      outputStream.Position = 0;
      return outputStream;
    }
    public async Task<Stream> ConvertImageFormatAsync(IFormFile file, string format)
    {
      var (isValid, errorMessage) = await ValidateImageAsync(file);
      if (!isValid)
        throw new InvalidOperationException(errorMessage);

      // Hiện tại chỉ hỗ trợ windows
      // Nếu cần hỗ trợ đa nền tảng, có thể cài thêm thư viện khác
      using var stream = file.OpenReadStream();
      using var image = Image.FromStream(stream);

      var outputStream = new MemoryStream();
      switch (format.ToLower())
      {
        case "jpeg":
        case "jpg":
          image.Save(outputStream, ImageFormat.Jpeg);
          break;
        case "png":
          image.Save(outputStream, ImageFormat.Png);
          break;
        default:
          throw new NotSupportedException("Định dạng không được hỗ trợ");
      }

      outputStream.Position = 0;
      return outputStream;
    }
  }
}
