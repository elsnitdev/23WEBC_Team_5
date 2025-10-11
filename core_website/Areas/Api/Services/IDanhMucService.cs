using core_website.Areas.Api.Models;

namespace core_website.Areas.Api.Services
{
  public interface IDanhMucService
  {
    List<DanhMuc> GetAll();
    DanhMuc? GetById(int id);
    void Categorize(int MaSP, int MaDM);
    void Add(DanhMuc danhMuc);
    void Update(DanhMuc danhMuc);
    void Delete(int id);
  }
}
