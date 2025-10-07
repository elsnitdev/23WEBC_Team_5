using Microsoft.AspNetCore.Http;// Tin: gọi để sử dụng IFormFile
using System.Threading.Tasks;
namespace core_website.Services
{
  //Tin 6/10
  // KhoaTr - 7/10/2025: Sửa interface IImageProcessingService, thêm/sửa 1 số phương thức
  public interface IImageProcessingService
  {
    // KhoaTr - 7/10/2025: Thêm phương thức ValidateImageAsync để kiểm tra tính hợp lệ của file ảnh
    // params:
    //    + file: file ảnh được upload từ form
    //    + maxSizeBytes: kích thước tối đa của file (tính bằng byte), nếu không truyền thì mặc định là 5MB
    // return: một tuple (bool IsValid, string ErrorMessage)
    Task<(bool IsValid, string ErrorMessage)> ValidateImageAsync(IFormFile file, long maxSizeBytes = 5 * 1024 * 1024);
    // KhoaTr - 7/10/2025: Sửa phương thức ProcessAndSaveImageAsync để xử lý và lưu ảnh
    // params:
    //    + file: file ảnh được upload từ form
    //    + destinationPath: đường dẫn thư mục để lưu file
    //    + name: tên file (không bao gồm phần định dạng), nếu null hoặc rỗng sẽ tự tạo tên ngẫu nhiên
    // return: đường dẫn của file đã được lưu
    Task<string> ProcessAndSaveImageAsync(IFormFile file, string destinationPath, string? name);
    // KhoaTr - 7/10/2025: Thêm phương thức ResizeImageAsync để thay đổi kích thước ảnh
    // params:
    //    + file: file ảnh được upload từ form
    //    + maxWidth: chiều rộng tối đa
    //    + maxHeight: chiều cao tối đa
    //    + preserveAspectRatio: có giữ tỉ lệ hình hay không, mặc định là true
    // return: một Stream chứa ảnh đã được thay đổi kích thước
    Task<Stream> ResizeImageAsync(IFormFile file, int maxWidth, int maxHeight, bool preserveAspectRatio = true);
    // KhoaTr - 7/10/2025: Thêm phương thức ConvertImageFormatAsync để chuyển đổi định dạng ảnh
    // params:
    //    + file: file ảnh được upload từ form
    //    + format: định dạng ảnh đích (ví dụ: "jpeg", "png", "bmp")
    // return: một Stream chứa ảnh đã được chuyển đổi định dạng
    Task<Stream> ConvertImageFormatAsync(IFormFile file, string format);
  }
    //Tin END
}
