// KhoaTr - 5/10/2025: Sửa lại namespace từ core_w2 thành core_website
using core_website.Models;

namespace core_website.Services
// KhoaTr - END
{
  public interface ISanPhamService
  {
    List<SanPham> GetAll();

    //Huy code
    void UpdateList(List<SanPham> list);
    //Huy end
  }
}
