namespace core_website.Areas.Admins.Services
{
  /// <summary>
  /// Dịch vụ xử lý ảnh.
  /// <remarks>Tin 6/10</remarks>
  /// <remarks>KhoaTr - 7/10/2025: Sửa interface IImageProcessingService, thêm/sửa 1 số phương thức</remarks>
  /// </summary>
  public interface IImageProcessingService
  {
    /// <summary>
    /// Kiểm tra tính hợp lệ của file ảnh.
    /// </summary>
    /// <param name="file">File ảnh được upload từ form.</param>
    /// <param name="maxSizeBytes">Kích thước tối đa của file (tính bằng byte), nếu không truyền thì mặc định là 5MB.</param>
    /// <returns>Một tuple (bool IsValid, string ErrorMessage).</returns>
    /// <remarks>KhoaTr - 7/10/2025: Thêm phương thức ValidateImageAsync để kiểm tra tính hợp lệ của file ảnh.</remarks>
    Task<(bool IsValid, string ErrorMessage)> ValidateImageAsync(IFormFile file, long maxSizeBytes = 5 * 1024 * 1024);

    /// <summary>
    /// Xử lý và lưu ảnh.
    /// </summary>
    /// <param name="file">File ảnh được upload từ form.</param>
    /// <param name="destinationPath">Đường dẫn thư mục để lưu file.</param>
    /// <param name="name">Tên file (không bao gồm phần định dạng), nếu null hoặc rỗng sẽ tự tạo tên ngẫu nhiên.</param>
    /// <returns>Đường dẫn của file đã được lưu.</returns>
    /// <remarks>KhoaTr - 7/10/2025: Sửa phương thức ProcessAndSaveImageAsync để xử lý và lưu ảnh.</remarks>
    Task<string> ProcessAndSaveImageAsync(IFormFile file, string destinationPath, string? name);

    /// <summary>
    /// Thay đổi kích thước ảnh.
    /// </summary>
    /// <param name="file">File ảnh được upload từ form.</param>
    /// <param name="maxWidth">Chiều rộng tối đa.</param>
    /// <param name="maxHeight">Chiều cao tối đa.</param>
    /// <param name="preserveAspectRatio">Có giữ tỉ lệ hình hay không, mặc định là true.</param>
    /// <returns>Một Stream chứa ảnh đã được thay đổi kích thước.</returns>
    /// <remarks>KhoaTr - 7/10/2025: Thêm phương thức ResizeImageAsync để thay đổi kích thước ảnh.</remarks>
    Task<Stream> ResizeImageAsync(IFormFile file, int maxWidth, int maxHeight, bool preserveAspectRatio = true);

    /// <summary>
    /// Chuyển đổi định dạng ảnh.
    /// </summary>
    /// <param name="file">File ảnh được upload từ form.</param>
    /// <param name="format">Định dạng ảnh đích (ví dụ: "jpeg", "png", "bmp").</param>
    /// <returns>Một Stream chứa ảnh đã được chuyển đổi định dạng.</returns>
    /// <remarks>KhoaTr - 7/10/2025: Thêm phương thức ConvertImageFormatAsync để chuyển đổi định dạng ảnh.</remarks>
    Task<Stream> ConvertImageFormatAsync(IFormFile file, string format);
  }
}