using core_website.Areas.Api.Models;

namespace core_website.Areas.Api.Services
{
  /// <summary>
  /// Dịch vụ liên quan đến người dùng.
  /// </summary>
  public interface INguoiDungService
  {
    /// <summary>
    /// Kiểm tra người dùng trong hệ thống
    /// </summary>
    /// <param name="data">Thông tin đăng nhập của người dùng</param>
    /// <returns>Thông tin người dùng nếu tồn tại, ngược lại trả về null</returns>
    NguoiDungResponse Login(NguoiDungLoginRequest data);
  }
}
