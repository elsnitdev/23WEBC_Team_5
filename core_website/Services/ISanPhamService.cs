using core_w2.Models;

namespace core_w2.Services
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
