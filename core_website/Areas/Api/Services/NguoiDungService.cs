using System.Data;
using core_website.Areas.Admins.Models;
using core_website.Areas.Api.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer.Server;

namespace core_website.Areas.Api.Services
{
  public class NguoiDungService : INguoiDungService
  {
    private readonly ApplicationDbContext _context;
    public NguoiDungService(IConfiguration configuration, ApplicationDbContext context)
    {
      _context = context;
    }
    public async Task<NguoiDungResponse> Login(NguoiDungLoginRequest data)
    {
      var user = await _context.NguoiDung.AsNoTracking().FirstOrDefaultAsync(u => u.TenND == data.TenND && VerifyPassword(data.MatKhau, u.MatKhau));

      var response = new NguoiDungResponse();

      if (user == null)
      {
        response.Success = false;
        response.Message = "Tên đăng nhập hoặc mật khẩu không đúng.";
        return null;
      }

      response.User = new NguoiDungInfo
      {
        MaND = user.MaND,
        TenND = user.TenND,
        VaiTro = user.VaiTro
      };

      return response;
    }
    // Kiểm tra mật khẩu
    private bool VerifyPassword(string plainPassword, string hashedPassword)
    {
      if (string.IsNullOrEmpty(hashedPassword))
        return false;

      return plainPassword == hashedPassword;
    }
  }
}
