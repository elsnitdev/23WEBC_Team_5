using core_website.Areas.Api.Models;
using core_website.Areas.Api.Services;
using Microsoft.Data.SqlClient;
using System.Data;

namespace core_website.Areas.Admins.Services
  {
    public class DanhMucService : IDanhMucService
    {
      private readonly string _connectionString;

      public DanhMucService(IConfiguration configuration)
      {
        _connectionString = configuration.GetConnectionString("DefaultConnectionString");
      }

      // Lấy tất cả danh mục
      public List<DanhMuc> GetAll()
      {
        var danhMucs = new List<DanhMuc>();

        const string query = "SELECT MaDM, TenDM = FROM DanhMuc";

        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand(query, connection);

        connection.Open();
        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
          danhMucs.Add(MapToDanhMuc(reader));
        }

        return danhMucs;
      }

      //  Lấy 1 danh mục theo ID
      public DanhMuc? GetById(int id)
      {
        const string query = "SELECT MaDM, TenDM FROM DanhMuc WHERE MaDM = @MaDanhMuc";

        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand(query, connection);
        command.Parameters.Add("@MaDanhMuc", SqlDbType.Int).Value = id;

        connection.Open();
        using var reader = command.ExecuteReader();

        if (reader.Read())
        {
          return MapToDanhMuc(reader);
        }

        return null;
      }

      // ✅ Thêm mới danh mục
      public void Add(DanhMuc danhMuc)
      {
        const string query = @"
                INSERT INTO DanhMuc (TenDM)
                VALUES (@TenDanhMuc);
                SELECT SCOPE_IDENTITY();
            ";

        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@TenDanhMuc", danhMuc.TenDM);


        connection.Open();
        danhMuc.MaDM = Convert.ToInt32(command.ExecuteScalar());
      }

      // ✅ Cập nhật danh mục
      public void Update(DanhMuc danhMuc)
      {
        const string query = @"
                UPDATE DanhMuc
                SET TenDM = @TenDanhMuc,
                WHERE MaDM = @MaDanhMuc
            ";

        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@MaDanhMuc", danhMuc.MaDM);
        command.Parameters.AddWithValue("@TenDanhMuc", danhMuc.MaDM);


        connection.Open();
        command.ExecuteNonQuery();
      }

      //  Xóa danh mục
      public void Delete(int id)
      {
        const string query = "DELETE FROM DanhMuc WHERE MaDM = @MaDanhMuc";

        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand(query, connection);
        command.Parameters.Add("@MaDanhMuc", SqlDbType.Int).Value = id;

        connection.Open();
        command.ExecuteNonQuery();
      }
    public void Categorize(int MaSP, int MaDM)
    {
      if (MaSP == null || MaDM == null)
      {
        throw new ArgumentException("Danh sách MaSP và MaDM không được rỗng.");
      }

      using (var connection = new SqlConnection(_connectionString))
      {
        connection.Open();

        // Validate MaSP
        var sanPhamQuery = $"SELECT COUNT(*) FROM SanPham WHERE MaSP = {MaSP}";
        using (var command = new SqlCommand(sanPhamQuery, connection))
        {
          var count = (int)command.ExecuteScalar();
          if (count == 0)
          {
            throw new ArgumentException("MaSP không tồn tại.");
          }
        }

        // Validate DSMaDM
        var danhMucQuery = $"SELECT COUNT(*) FROM DanhMuc WHERE MaDM = {MaDM}";
        using (var command = new SqlCommand(danhMucQuery, connection))
        {
          var count = (int)command.ExecuteScalar();
          if (count == 0)
          {
            throw new ArgumentException("MaDM không tồn tại.");
          }
        }


        var phanLoaiQuery = $"SELECT COUNT(*) FROM PhanLoai WHERE MaSP = {MaSP} AND MaDM = {MaDM}";
        using (var command = new SqlCommand(phanLoaiQuery, connection))
        {
          var count = (int)command.ExecuteScalar();
          if (count > 0)
          {
            return; // Pair already exists, no need to insert
          }
        }

        var insertQuery = $"INSERT INTO PhanLoai (MaSP, MaDM) VALUES ({MaSP}, {MaDM})";
        using (var command = new SqlCommand(insertQuery, connection))
        {
          command.ExecuteNonQuery();
        }
      }
    }

    //  Hàm ánh xạ dữ liệu từ SQL -> Model
    private DanhMuc MapToDanhMuc(SqlDataReader reader)
      {
        return new DanhMuc
        {
          MaDM = reader.GetInt32(reader.GetOrdinal("MaDanhMuc")),
          TenDM = reader.GetString(reader.GetOrdinal("TenDanhMuc")),
        };
      }
    }
  }