using System.Data;
using core_website.Areas.Api.Models;
using Microsoft.Data.SqlClient;

namespace core_website.Areas.Api.Services
{
  public class ThongKeService : IThongKeService
  {
    public ThongKeService(IConfiguration configuration)
    {

    }
    public List<ThongKeDanhMuc> GetCategoryStatistics()
    {
        //string sql = "" +
        //  "SELECT d.MaDM, d.TenDM, COUNT(pl.MaSP) AS SoLuongSanPham " +
        //  "FROM DanhMuc d LEFT JOIN PhanLoai pl ON " +
        //  "d.MaDM = pl.MaDM " +
        //  "GROUP BY d.MaDM, d.TenDM " +
        //  "ORDER BY d.MaDM "
        //;

        return null;
    }
    private ThongKeDanhMuc MapToThongKeDanhMuc(SqlDataReader reader)
    {
      return new ThongKeDanhMuc
      {
        MaDM = reader.GetInt32("MaDM"),
        TenDM = reader.GetString("TenDM"),
        SoLuongSanPham = reader.GetInt32("SoLuongSanPham")
      };
    }
  }
}
