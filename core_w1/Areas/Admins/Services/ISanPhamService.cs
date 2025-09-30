using core_w2.Models;

namespace core_w2.Areas.Admins.Services
{
  public interface ISanPhamService
    {
        void LuuSanPham(SanPham sp);
        IEnumerable<SanPham> GetAll();
    }
}
