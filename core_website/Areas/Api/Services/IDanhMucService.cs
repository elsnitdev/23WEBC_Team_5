using core_website.Areas.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace core_website.Areas.Api.Services
{
  /// <summary>
  /// Dịch vụ quản lý danh mục sản phẩm.
  /// </summary>
  public interface IDanhMucService
  {
        /// <summary>
        /// Lấy danh sách tất cả các danh mục.
        /// </summary>
        /// <returns>Danh sách các đối tượng <see cref="DanhMuc"/>.</returns>
        Task<ActionResult<IEnumerable<DanhMuc>>> GetAll();

    /// <summary>
    /// Lấy thông tin danh mục theo ID.
    /// </summary>
    /// <param name="id">ID của danh mục cần tìm.</param>
    /// <returns>Đối tượng <see cref="DanhMuc"/> tương ứng với ID, hoặc null nếu không tìm thấy.</returns>
    DanhMuc? GetById(int id);

    /// <summary>
    /// Gán một sản phẩm vào một danh mục cụ thể.
    /// </summary>
    /// <param name="MaSP">Mã sản phẩm (ID của sản phẩm).</param>
    /// <param name="MaDM">Mã danh mục (ID của danh mục).</param>
    /// <remarks>
    /// Phương thức này dùng để liên kết sản phẩm với danh mục, thường dùng trong phân loại sản phẩm.
    /// </remarks>
    void Categorize(int MaSP, int MaDM);

    /// <summary>
    /// Thêm một danh mục mới vào hệ thống.
    /// </summary>
    /// <param name="danhMuc">Đối tượng <see cref="DanhMuc"/> chứa thông tin danh mục cần thêm.</param>
    /// <returns>Đối tượng <see cref="DanhMuc"/> đã được thêm, bao gồm ID mới được tạo.</returns>
    DanhMuc Add(DanhMuc danhMuc);

    /// <summary>
    /// Cập nhật thông tin của một danh mục hiện có.
    /// </summary>
    /// <param name="danhMuc">Đối tượng <see cref="DanhMuc"/> với thông tin đã được cập nhật.</param>
    void Update(DanhMuc danhMuc);

    /// <summary>
    /// Xóa một danh mục theo ID.
    /// </summary>
    /// <param name="id">ID của danh mục cần xóa.</param>
    /// <remarks>
    /// Cảnh báo: Việc xóa danh mục có thể ảnh hưởng đến các sản phẩm đang thuộc danh mục này.
    /// </remarks>
    void Delete(int id);
  }
}