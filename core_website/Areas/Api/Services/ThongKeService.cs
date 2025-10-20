using System.Data;
using core_website.Areas.Api.Models;
using Microsoft.Data.SqlClient;

namespace core_website.Areas.Api.Services
{
  public class ThongKeService : IThongKeService
  {
    private readonly string _connectionString;
    public ThongKeService(IConfiguration configuration)
    {
      _connectionString = configuration.GetConnectionString("DefaultConnectionString");
    }
    public List<ThongKeDanhMuc> GetCategoryStatistics()
    {
      var result = new List<ThongKeDanhMuc>();
      try
      {
        using (var connection = new SqlConnection(_connectionString))
        {
          string sql = "" +
            "SELECT d.MaDM, d.TenDM, COUNT(pl.MaSP) AS SoLuongSanPham " +
            "FROM DanhMuc d LEFT JOIN PhanLoai pl ON " +
            "d.MaDM = pl.MaDM " +
            "GROUP BY d.MaDM, d.TenDM " +
            "ORDER BY d.MaDM "
          ;

          var cmd = new SqlCommand(sql, connection);
          connection.Open();

          using (var reader = cmd.ExecuteReader())
          {
            while (reader.Read())
            {
              result.Add(MapToThongKeDanhMuc(reader));
            }
          }
        }
      }
      catch (Exception ex)
      {
        throw new Exception("Error getting the statistic", ex);
      }
      return result;
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
