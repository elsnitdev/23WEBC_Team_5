using core_website.Areas.Admins.Models;
using core_website.Areas.Api.Models;

namespace core_website.Areas.Api.Services
{
  public interface INguoiDungService
  {
    NguoiDungResponse Login(NguoiDungLoginRequest data);

        //Tan 11/10/2025 - Chỉnh sửa INguoiDungService
        List<NguoiDung> GetAll();
        NguoiDung? GetById(int id);
        bool Create(NguoiDung nguoiDung);
        bool Update(NguoiDung nguoiDung);
        bool Delete(int id);
        //Tan END
    }
}
