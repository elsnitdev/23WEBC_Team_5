using core_website.Models;

namespace core_website.Services
{
  public interface ISanPhamService
  {
    List<SanPham> GetAll();

    //Huy code
    void UpdateList(List<SanPham> list);
    //Huy end
  }
}
