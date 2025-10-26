using core_website.Areas.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace core_website.Areas.Api.Services
{
  /// <summary>
  /// Dịch vụ liên quan đến quản lý sản phẩm.
  /// </summary>
  /// <remarks>KhoaTr - 5/10/2025: Sửa lại namespace từ core_w2 thành core_website.</remarks>
  public interface ISanPhamService
  {
    /// <summary>
    /// Lấy danh sách tất cả sản phẩm.
    /// </summary>
    /// <param name="itemsPerPage">Số lượng sản phẩm cần lấy mỗi trang. Nếu null, lấy tất cả sản phẩm.</param>
    /// <returns>Danh sách các sản phẩm.</returns>
    /// <remarks>Huy - 11/10/25: Thêm itemsPerPage để lấy số lượng item cần để hiển thị/trang.</remarks>
    Task<IEnumerable<object>> GetAll(int? itemsPerPage = null);

    /// <summary>
    /// Lấy thông tin sản phẩm theo ID.
    /// </summary>
    /// <param name="id">ID của sản phẩm cần lấy.</param>
    /// <returns>Đối tượng sản phẩm tương ứng với ID, hoặc null nếu không tìm thấy.</returns>
    Task<SanPham?> GetById(int id);

    /// <summary>
    /// Tìm kiếm sản phẩm theo từ khóa.
    /// </summary>
    /// <param name="keyword">Từ khóa để tìm kiếm sản phẩm (ví dụ: tên sản phẩm).</param>
    /// <returns>Danh sách các sản phẩm khớp với từ khóa.</returns>
    List<SanPham> Search(string keyword);

    /// <summary>
    /// Thêm một sản phẩm mới.
    /// </summary>
    /// <param name="sp">Đối tượng sản phẩm cần thêm.</param>
    Task<SanPham> Add(SanPham sanPhamMoi, List<int> danhMucIds);

    /// <summary>
    /// Cập nhật đường dẫn hình ảnh của một sản phẩm.
    /// </summary>
    /// <param name="MaSP">Mã sản phẩm của sản phẩm sẽ được cập nhật</param>
    /// <param name="imagePaths">Đường dẫn hình ảnh mới của sản phẩm, là chuỗi liên tiếp các nhau dấu ';'</param>
    /// <returns>Sản phẩm đã được cập nhật hình ảnh.</returns>
    Task<SanPham> UpdateImage(int MaSP, string imagePaths);

    /// <summary>
    /// Xóa một sản phẩm theo ID.
    /// </summary>
    /// <param name="id">ID của sản phẩm cần xóa.</param>
    void Delete(int id);

    /// <summary>
    /// Lấy danh sách các sản phẩm có khuyến mãi.
    /// </summary>
    /// <returns>Danh sách các sản phẩm đang có khuyến mãi.</returns>
    List<SanPham> GetKhuyenMai();

    /// <summary>
    /// Cập nhật danh sách các sản phẩm.
    /// </summary>
    /// <param name="list">Danh sách các sản phẩm cần cập nhật.</param>
    /// <remarks>Huy code.</remarks>
    void UpdateList(List<SanPham> list);

    /// <summary>
    /// Lấy ID của sản phẩm mới nhất.
    /// </summary>
    /// <returns>ID của sản phẩm mới nhất.</returns>
    /// <remarks>Huy code.</remarks>
    int GetLastestProductId();    
  }
}