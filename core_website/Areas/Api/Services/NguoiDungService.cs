using System.Data;
using core_website.Areas.Admins.Models;
using core_website.Areas.Api.Models;
using Microsoft.Data.SqlClient;
using Microsoft.SqlServer.Server;

namespace core_website.Areas.Api.Services
{
  public class NguoiDungService : INguoiDungService
  {
    private readonly string _connectionString;
    public NguoiDungService(IConfiguration configuration)
    {
      _connectionString = configuration.GetConnectionString("DefaultConnectionString");
    }
    public NguoiDungResponse Login(NguoiDungLoginRequest data)
    {
      var result = new NguoiDungResponse();
      try
      {
        using (var connection = new SqlConnection(_connectionString))
        {
          var cmd = new SqlCommand(@"" +
            "SELECT * " +
            "FROM NguoiDung " +
            "WHERE TenND LIKE @TenND AND TrangThai = 1"
          , connection);

          cmd.Parameters.AddWithValue("@TenND", data.TenND ?? string.Empty);

          connection.Open();

          using (var reader = cmd.ExecuteReader())
          {
            if (reader.Read())
            {
              var user = MapToNguoiDung(reader);

              if (VerifyPassword(data.MatKhau, user.MatKhau))
              {
                result.Success = true;
                result.User = new NguoiDungInfo(){
                  TenND = user.TenND,
                  MaND = user.MaND,
                  VaiTro = user.VaiTro
                };
                result.Message = "Đăng nhập thành công";
              }
              else
              {
                result.Success = false;
                result.Message = "Tên đăng nhập hoặc mật khẩu không đúng";
              }
            }
            else
            {
              result.Success = false;
              result.Message = "Tên đăng nhập hoặc mật khẩu không đúng";
            }
          }
        }
      }
      catch (Exception ex)
      {
        throw new Exception("Error authenticating", ex);
      }
      return result;
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
