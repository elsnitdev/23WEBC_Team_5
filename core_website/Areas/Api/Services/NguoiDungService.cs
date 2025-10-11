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
    //Tan - 11/10/2025 - Chỉnh sửa NguoiDungService
    public List<NguoiDung> GetAll()
    {
        var list = new List<NguoiDung>();
        using (var conn = new SqlConnection(_connectionString))
        {
            conn.Open();
            using var cmd = new SqlCommand("SELECT * FROM NguoiDung", conn);
            using var r = cmd.ExecuteReader();
            while (r.Read())
            {
                list.Add(MapToNguoiDung(r));
            }
        }
        return list;
    }

    public NguoiDung? GetById(int id)
    {
        using var conn = new SqlConnection(_connectionString);
        conn.Open();
        using var cmd = new SqlCommand("SELECT * FROM NguoiDung WHERE MaND = @id", conn);
        cmd.Parameters.AddWithValue("@id", id);
        using var r = cmd.ExecuteReader();
        if (r.Read()) return MapToNguoiDung(r);
        return null;
    }

    public bool Create(NguoiDung nguoiDung)
    {
        using var conn = new SqlConnection(_connectionString);
        conn.Open();
        using var cmd = new SqlCommand(
            "INSERT INTO NguoiDung (TenND, MatKhau, VaiTro, TrangThai) VALUES (@TenND, @MatKhau, @VaiTro, @TrangThai)",
            conn);
        cmd.Parameters.AddWithValue("@TenND", nguoiDung.TenND);
        cmd.Parameters.AddWithValue("@MatKhau", nguoiDung.MatKhau);
        cmd.Parameters.AddWithValue("@VaiTro", nguoiDung.VaiTro);
        cmd.Parameters.AddWithValue("@TrangThai", nguoiDung.TrangThai);
        return cmd.ExecuteNonQuery() > 0;
    }

    public bool Update(NguoiDung nguoiDung)
    {
        using var conn = new SqlConnection(_connectionString);
        conn.Open();
        using var cmd = new SqlCommand(
            "UPDATE NguoiDung SET TenND=@TenND, MatKhau=@MatKhau, VaiTro=@VaiTro, TrangThai=@TrangThai WHERE MaND=@MaND",
            conn);
        cmd.Parameters.AddWithValue("@TenND", nguoiDung.TenND);
        cmd.Parameters.AddWithValue("@MatKhau", nguoiDung.MatKhau);
        cmd.Parameters.AddWithValue("@VaiTro", nguoiDung.VaiTro);
        cmd.Parameters.AddWithValue("@TrangThai", nguoiDung.TrangThai);
        cmd.Parameters.AddWithValue("@MaND", nguoiDung.MaND);
        return cmd.ExecuteNonQuery() > 0;
    }

    public bool Delete(int id)
    {
        using var conn = new SqlConnection(_connectionString);
        conn.Open();
        using var cmd = new SqlCommand("DELETE FROM NguoiDung WHERE MaND = @id", conn);
        cmd.Parameters.AddWithValue("@id", id);
        return cmd.ExecuteNonQuery() > 0;
    }
        //Tan END
        // Map dữ liệu từ SqlDataReader sang đối tượng NguoiDung
        private NguoiDung MapToNguoiDung(SqlDataReader reader)
    {
      return new NguoiDung
      {
        MaND = Convert.ToInt32(reader["MaND"]), //Phải Convert qua
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
