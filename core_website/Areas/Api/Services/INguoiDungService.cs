using core_website.Areas.Admins.Models;
using core_website.Areas.Api.Models;

namespace core_website.Areas.Api.Services
{
  public interface INguoiDungService
  {
    NguoiDungResponse Login(NguoiDungLoginRequest data);
  }
}
