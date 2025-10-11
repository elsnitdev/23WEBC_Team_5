// KhoaTr - 5/10/2025: Sửa lại namespace từ core_w2 thành core_website
using core_website.Areas.Api.Models;

namespace core_website.Areas.Api.Services
// KhoaTr - END
{
  public interface ISanPhamService
  {
        /// Lấy tất cả sản phẩm
        /// Huy -11/10/25: thêm itemsPerPage để lấy số lượng item cần để hiển thị/trang
        List<SanPham> GetAll(int? itemsPerPage = null); 
        /// Lấy sản phẩm theo ID
        SanPham? GetById(int id);
        /// Tìm kiếm sản phẩm theo tên (hoặc keyword)
        List<SanPham> Search(string keyword);
        /// Thêm sản phẩm mới
        void Add(SanPham sp);
        /// Cập nhật thông tin sản phẩm
        void Update(SanPham sp);
        /// Xóa sản phẩm theo ID
        void Delete(int id);
        /// Lấy top sản phẩm có khuyến mãi
        List<SanPham> GetKhuyenMai();
        //Huy code
        void UpdateList(List<SanPham> list);
        //Huy end
  }
}
