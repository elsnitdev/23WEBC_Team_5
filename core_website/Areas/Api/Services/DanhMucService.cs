using core_website.Areas.Api.Models;
using core_website.Areas.Api.Services;
using Microsoft.Data.SqlClient;
using System.Data;

namespace core_website.Areas.Admins.Services
  {
    public class DanhMucService : IDanhMucService
    {
      private readonly string _connectionString;
      private readonly ILogger<DanhMucService> _logger;

      public DanhMucService(
        IConfiguration configuration,
        ILogger<DanhMucService> logger
      )
      {
        _connectionString = configuration.GetConnectionString("DefaultConnectionString");
        _logger = logger;
      }

      // Lấy tất cả danh mục
      public List<DanhMuc> GetAll()
      {
        var danhMucs = new List<DanhMuc>();

        const string query = "SELECT MaDM, TenDM FROM DanhMuc";

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

      // Thêm mới danh mục
      public DanhMuc Add(DanhMuc danhMuc)
      {
            // 19/10 trung tin: thêm kiểm tra trùng tên danh mục
            const string checkQuery = @"
        SELECT TOP 1 MaDM FROM DanhMuc WHERE TenDM = @TenDanhMuc
    ";
            const string query = @"
                INSERT INTO DanhMuc (TenDM)
                VALUES (@TenDanhMuc);
                SELECT SCOPE_IDENTITY();
            ";

            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            // 19/10 trung tin: thêm kiểm tra trùng tên danh mục
            using var checkCommand = new SqlCommand(checkQuery, connection);

            checkCommand.Parameters.AddWithValue("@TenDanhMuc", danhMuc.TenDM);
            var existId = checkCommand.ExecuteScalar();
            if (existId != null)
            {
                throw new InvalidOperationException("Tên danh mục đã tồn tại.");
            }
            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TenDanhMuc", danhMuc.TenDM);


            danhMuc.MaDM = Convert.ToInt32(command.ExecuteScalar());
      _logger.LogInformation($"Thêm danh mục có mã {danhMuc.MaDM} thành công");
      return danhMuc;
    }

      // Cập nhật danh mục
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
            throw new InvalidOperationException("MaSP không tồn tại.");
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
            _logger.LogInformation($"Cặp MaSP: {MaSP} và MaDM: {MaDM} đã tồn tại trong bảng PhanLoai.");
            return;
          }
        }

        var insertQuery = $"INSERT INTO PhanLoai (MaSP, MaDM) VALUES ({MaSP}, {MaDM})";
        using (var command = new SqlCommand(insertQuery, connection))
        {
          command.ExecuteNonQuery();
          _logger.LogInformation($"Đã thêm cặp MaSP: {MaSP} và MaDM: {MaDM} vào bảng PhanLoai.");
        }
      }
    }

    //  Hàm ánh xạ dữ liệu từ SQL -> Model
    private DanhMuc MapToDanhMuc(SqlDataReader reader)
      {
        return new DanhMuc
        {
          MaDM = reader.GetInt32(reader.GetOrdinal("MaDM")),
          TenDM = reader.GetString(reader.GetOrdinal("TenDM")),
        };
      }
    }
  }