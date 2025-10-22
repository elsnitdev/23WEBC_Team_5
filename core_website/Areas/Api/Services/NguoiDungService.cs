using System.Data;
using core_website.Areas.Admins.Models;
using core_website.Areas.Api.Models;
using Microsoft.Data.SqlClient;
using Microsoft.SqlServer.Server;

namespace core_website.Areas.Api.Services
{
  public class NguoiDungService : INguoiDungService
  {
    public NguoiDungService(IConfiguration configuration)
    {

    }
    public NguoiDungResponse Login(NguoiDungLoginRequest data)
    {
        return null;
    }
    // Map dữ liệu từ SqlDataReader sang đối tượng NguoiDung
    private NguoiDung MapToNguoiDung(SqlDataReader reader)
    {
      return new NguoiDung
      {
        MaND = reader.GetInt32("MaND"),
        TenND = reader.GetString("TenND"),
        MatKhau = reader.GetString("MatKhau"), // Don't return password in production
        VaiTro = reader.IsDBNull("VaiTro") ? null : reader.GetString("VaiTro"),
        TrangThai = reader.GetBoolean("TrangThai")
      };
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
