using core_website.Areas.Api.Models;

namespace core_website.Areas.Api.Services
{
  /// <summary>
  /// Dịch vụ thống kê
  /// </summary>
  public interface IThongKeService
  {
    /// <summary>
    /// Thống kê thông tin liên quan đến danh mục sản phẩm
    /// </summary>
    /// <returns>Danh sách các đối tượng <see cref="ThongKeDanhMuc"/> chứa thông tin thống kê.</returns></returns>
    /// <returns></returns>
    List<ThongKeDanhMuc> GetCategoryStatistics();
  }
}
