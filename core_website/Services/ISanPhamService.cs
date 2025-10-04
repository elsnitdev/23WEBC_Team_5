// KhoaTr - 5/10/2025: Sửa lại namespace từ core_w2 thành core_website
using core_website.Models;

namespace core_website.Services
// KhoaTr - END
{
  public interface ISanPhamService
  {
        /// Lấy tất cả sản phẩm
        List<SanPham> GetAll();
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
