using core_w2.Models;

namespace core_w2.Services
{
  public interface ISanPhamService
  {
    List<SanPham> GetAll();

    //Huy code
    void UpdateList(List<SanPham> list);
    //Huy end
  }
}
