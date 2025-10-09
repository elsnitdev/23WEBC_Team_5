using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Processing;

namespace core_website.Areas.Admins.Services
{
  public class ImageProcessingService : IImageProcessingService
  {
    // Kiểm tra tính hợp lệ của tệp hình ảnh
    public async Task<(bool IsValid, string ErrorMessage)> ValidateImageAsync(IFormFile file, long maxSizeBytes = 5 * 1024 * 1024)
    {
      // Kiểm tra nếu tệp rỗng hoặc null
      if (file == null || file.Length == 0)
        return (false, "Không có tệp nào được tải lên");

      // Kiểm tra kích thước tệp
      if (file.Length > maxSizeBytes)
        return (false, $"Kích thước tệp vượt quá giới hạn {maxSizeBytes / (1024 * 1024)} MB");

      // Kiểm tra định dạng tệp (phần mở rộng)
      var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };
      var extension = Path.GetExtension(file.FileName).ToLower();
      if (!allowedExtensions.Contains(extension))
        return (false, "Định dạng tệp không được hỗ trợ");

      // Kiểm tra xem tệp có phải là hình ảnh hợp lệ không
      try
      {
        using var image = await Image.LoadAsync(file.OpenReadStream());
        return (true, string.Empty);
      }
      catch (Exception)
      {
        return (false, "Tệp không phải là hình ảnh hợp lệ");
      }
    }

    // Xử lý và lưu hình ảnh vào thư mục chỉ định
    public async Task<string> ProcessAndSaveImageAsync(IFormFile file, string destinationPath, string? fileName)
    {
      // Kiểm tra tính hợp lệ của tệp trước khi lưu
      var (isValid, errorMessage) = await ValidateImageAsync(file);
      if (!isValid)
        throw new InvalidOperationException(errorMessage);

      // Tạo thư mục đích nếu chưa tồn tại
      if (!Directory.Exists(destinationPath))
      {
        Directory.CreateDirectory(destinationPath);
      }

      // Tạo tên tệp duy nhất để tránh ghi đè
      var uniqueFileName = $"{(fileName != null ? fileName : Guid.NewGuid())}{Path.GetExtension(file.FileName)}";
      var filePath = Path.Combine(destinationPath, uniqueFileName);

      // Lưu tệp vào đường dẫn được chỉ định
      using var stream = new FileStream(filePath, FileMode.Create);
      await file.CopyToAsync(stream);

      return filePath; // Trả về đường dẫn của tệp đã lưu
    }

    // Thay đổi kích thước hình ảnh
    public async Task<Stream> ResizeImageAsync(IFormFile file, int maxWidth, int maxHeight, bool preserveAspectRatio = true)
    {
      // Kiểm tra tính hợp lệ của tệp trước khi xử lý
      var (isValid, errorMessage) = await ValidateImageAsync(file);
      if (!isValid)
        throw new InvalidOperationException(errorMessage);

      // Tải hình ảnh từ luồng
      using var image = await Image.LoadAsync(file.OpenReadStream());
      int targetWidth = maxWidth;
      int targetHeight = maxHeight;

      // Tính toán kích thước mới nếu giữ tỉ lệ hình
      if (preserveAspectRatio)
      {
        var ratioX = (double)maxWidth / image.Width;
        var ratioY = (double)maxHeight / image.Height;
        var ratio = Math.Min(ratioX, ratioY);
        targetWidth = (int)(image.Width * ratio);
        targetHeight = (int)(image.Height * ratio);
      }

      // Thực hiện thay đổi kích thước hình ảnh
      image.Mutate(x => x.Resize(new ResizeOptions
      {
        Size = new Size(targetWidth, targetHeight),
        Mode = preserveAspectRatio ? ResizeMode.Max : ResizeMode.Stretch,
        Sampler = KnownResamplers.Lanczos3 // Sử dụng thuật toán Lanczos3 cho chất lượng cao
      }));

      // Lưu hình ảnh đã thay đổi vào luồng đầu ra
      var outputStream = new MemoryStream();
      await image.SaveAsync(outputStream, image.Metadata.DecodedImageFormat ?? PngFormat.Instance);
      outputStream.Position = 0;
      return outputStream; // Trả về luồng chứa hình ảnh đã thay đổi kích thước
    }

    // Chuyển đổi định dạng hình ảnh
    public async Task<Stream> ConvertImageFormatAsync(IFormFile file, string format)
    {
      // Kiểm tra tính hợp lệ của tệp trước khi xử lý
      var (isValid, errorMessage) = await ValidateImageAsync(file);
      if (!isValid)
        throw new InvalidOperationException(errorMessage);

      // Tải hình ảnh từ luồng
      using var image = await Image.LoadAsync(file.OpenReadStream());
      var outputStream = new MemoryStream();

      // Chọn định dạng đầu ra dựa trên tham số format
      IImageFormat imageFormat = format.ToLower() switch
      {
        "jpeg" or "jpg" => JpegFormat.Instance,
        "png" => PngFormat.Instance,
        _ => throw new NotSupportedException("Định dạng không được hỗ trợ")
      };

      // Lưu hình ảnh với định dạng mới vào luồng đầu ra
      await image.SaveAsync(outputStream, imageFormat);
      outputStream.Position = 0;
      return outputStream; // Trả về luồng chứa hình ảnh với định dạng mới
    }
  }
}