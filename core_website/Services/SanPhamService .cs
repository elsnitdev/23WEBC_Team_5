// KhoaTr - 5/10/2025: Sửa lại namespace từ core_w2 thành core_website
using core_website.Models;
namespace core_website.Services;
// KhoaTr - END

public class SanPhamService : ISanPhamService
{
  private List<SanPham> _dsSanPham;
  public SanPhamService()
  {
    _dsSanPham = new List<SanPham>();
  }
  public SanPhamService(List<SanPham> dsSanPham)
  {
    _dsSanPham = dsSanPham;
  }
  public List<SanPham> GetAll() => _dsSanPham;

  //Huy code
  public void UpdateList(List<SanPham> list)
  {
    _dsSanPham.Clear();
    _dsSanPham.AddRange(list);
  }
  //Huy end
}
