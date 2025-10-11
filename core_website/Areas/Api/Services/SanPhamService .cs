// KhoaTr - 5/10/2025: Sửa lại namespace từ core_w2 thành core_website
using core_website.Areas.Api.Models;
using core_website.Areas.Api.Services;
using Humanizer;
using Microsoft.Data.SqlClient;
using System.Data;
namespace core_website.Services;
// KhoaTr - END

public class SanPhamService : ISanPhamService
{
  //Huy - 06/10/25
  private readonly string _connectionString;
  public SanPhamService(IConfiguration configuration)
  {
    _connectionString = configuration.GetConnectionString("DefaultConnectionString");
  }
  public List<SanPham> GetAll(int? itemsPerPage = null)
  {
    var result = new List<SanPham>();
    try
    {
      using (var connection = new SqlConnection(_connectionString))
      {
        //Huy - 11/10/25: sửa câu truy vấn chỉ lấy số lượng item cần
        string sql = itemsPerPage != null ? "SELECT TOP (@Ipp) *" : "SELECT *";
        sql += " FROM SanPham ORDER BY ThoiGianTao DESC";

        var cmd = new SqlCommand(sql, connection);
        if (itemsPerPage != null)
        {
            cmd.Parameters.AddWithValue("@Ipp", itemsPerPage);
        }

        connection.Open();

        using (var reader = cmd.ExecuteReader())
        {
          while (reader.Read())
          {
            result.Add(MapToSanPham(reader));
          }
        }
      }
    }
    catch (Exception ex)
    {
      throw new Exception("Error getting all products", ex);
    }
    return result;
  }

  public void Add(SanPham sanPhamMoi)
  {
    try
    {
      using (var connection = new SqlConnection(_connectionString))
      {
        var cmd = new SqlCommand(@"
                    INSERT INTO SanPham (TenSp, DonGia, KhuyenMai, MoTa, ThongSo, Tag, 
                    SoLuong, HinhAnh, ThoiGianTao, ThoiGianCapNhat, TrangThai)
                    VALUES (@TenSp, @DonGia, @KhuyenMai, @MoTa, @ThongSo, @Tag, 
                    @SoLuong, @HinhAnh, @ThoiGianTao, @ThoiGianCapNhat, @TrangThai);
                    SELECT SCOPE_IDENTITY();
                ", connection);

        AddSanPhamParameters(cmd, sanPhamMoi);
        connection.Open();
        sanPhamMoi.MaSP = Convert.ToInt32(cmd.ExecuteScalar());
      }
    }
    catch (Exception ex)
    {
      throw new Exception("Error adding product", ex);
    }
  }

  private SanPham MapToSanPham(SqlDataReader reader)
  {
    return new SanPham
    {
      MaSP = reader.GetInt32("MaSP"),
      TenSP = reader.IsDBNull(reader.GetOrdinal("TenSP")) ? null : reader.GetString("TenSP"),
      DonGia = reader.GetDecimal("DonGia"),
      KhuyenMai = reader.GetDecimal("KhuyenMai"),
      MoTa = reader.IsDBNull(reader.GetOrdinal("MoTa")) ? null : reader.GetString("MoTa"),
      ThongSo = reader.IsDBNull(reader.GetOrdinal("ThongSo")) ? null : reader.GetString("ThongSo"),
      Tag = reader.IsDBNull(reader.GetOrdinal("Tag")) ? null : reader.GetString("Tag"),
      SoLuong = reader.GetInt32("SoLuong"),
      HinhAnh = reader.IsDBNull(reader.GetOrdinal("HinhAnh")) ? null : reader.GetString("HinhAnh"),
      ThoiGianTao = reader.GetDateTime("ThoiGianTao"),
      ThoiGianCapNhat = reader.GetDateTime("ThoiGianCapNhat"),
      TrangThai = reader.GetBoolean("TrangThai")
    };
  }

  private void AddSanPhamParameters(SqlCommand command, SanPham sp)
  {
    command.Parameters.AddWithValue("@TenSP", sp.TenSP ?? (object)DBNull.Value);
    command.Parameters.AddWithValue("@DonGia", sp.DonGia);
    command.Parameters.AddWithValue("@KhuyenMai", sp.KhuyenMai);
    command.Parameters.AddWithValue("@MoTa", sp.MoTa ?? (object)DBNull.Value);
    command.Parameters.AddWithValue("@ThongSo", sp.ThongSo ?? (object)DBNull.Value);
    command.Parameters.AddWithValue("@Tag", sp.Tag ?? (object)DBNull.Value);
    command.Parameters.AddWithValue("@SoLuong", sp.SoLuong);
    command.Parameters.AddWithValue("@HinhAnh", sp.HinhAnh ?? (object)DBNull.Value);
    command.Parameters.AddWithValue("@ThoiGianTao", sp.ThoiGianTao);
    command.Parameters.AddWithValue("@ThoiGianCapNhat", sp.ThoiGianCapNhat);
    command.Parameters.AddWithValue("@TrangThai", sp.TrangThai);
  }

  public void UpdateList(List<SanPham> list)
  {
    //_dsSanPham.Clear();
    //_dsSanPham.AddRange(list);
  }
  public int GetLastestProductId()
  {
    try
    {
      using (var connection = new SqlConnection(_connectionString))
      {
        var cmd = new SqlCommand(@"
          SELECT TOP 1 MaSP FROM SanPham ORDER BY MaSP DESC
        ", connection);

        connection.Open();
        return Convert.ToInt32(cmd.ExecuteScalar());
      }
    }
    catch (Exception ex)
    {
      throw new Exception("Error adding product", ex);
    }
  }
  public SanPham? GetById(int id)
  {
    return null;
  }

  public List<SanPham> Search(string keyword)
  {
    return null;
  }

  public void Update(SanPham sp) { }

  public void Delete(int id) { }

  public List<SanPham> GetKhuyenMai()
  {
    return null;
  }
  //Huy end
}
