// KhoaTr - 5/10/2025: Sửa lại namespace từ core_w2 thành core_website
using core_website.Areas.Api.Models;
using core_website.Areas.Api.Services;
using Microsoft.Data.SqlClient;
using System.Data;
namespace core_website.Services;
// KhoaTr - END

public class SanPhamService : ISanPhamService
{
  public SanPhamService(IConfiguration configuration)
  {

  }
  public List<SanPham> GetAll(int? itemsPerPage = null)
  {
    
        //string sql = itemsPerPage != null ? "SELECT TOP (@Ipp) *" : "SELECT *";
        //sql += " FROM SanPham ORDER BY ThoiGianTao DESC";
    return null;
  }

  public SanPham Add(SanPham sanPhamMoi)
  {
        //var cmd = new SqlCommand(@"
        //            INSERT INTO SanPham (TenSp, DonGia, KhuyenMai, MoTa, ThongSo, Tag, 
        //            SoLuong, HinhAnh, ThoiGianTao, ThoiGianCapNhat, TrangThai)
        //            VALUES (@TenSp, @DonGia, @KhuyenMai, @MoTa, @ThongSo, @Tag, 
        //            @SoLuong, @HinhAnh, @ThoiGianTao, @ThoiGianCapNhat, @TrangThai);
        //            SELECT SCOPE_IDENTITY();
        //        ", connection);
        return null;
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
        //var cmd = new SqlCommand(@"
        //  SELECT TOP 1 MaSP FROM SanPham ORDER BY MaSP DESC
        //", connection);
        return 1;
  }
  public SanPham? GetById(int id)
  {
    return null;
  }

  public List<SanPham> Search(string keyword)
  {
    return null;
  }

  public SanPham UpdateImage(int MaSP, string imagePaths) {
        //var cmd = new SqlCommand(@"
        //  UPDATE SanPham
        //  SET HinhAnh = @HinhAnh
        //  WHERE MaSP = @MaSP;
        //  SELECT * FROM SanPham WHERE MaSP = @MaSP;
        //", connection);

        return null;
  }

  public void Delete(int id) { }

  public List<SanPham> GetKhuyenMai()
  {
    return null;
  }
  //Huy end
}
