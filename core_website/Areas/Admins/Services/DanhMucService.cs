using core_website.Areas.Admins.Models;
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
            command.Parameters.AddWithValue("@TenDanhMuc", danhMuc.TenDanhMuc);
            

            connection.Open();
            danhMuc.MaDanhMuc = Convert.ToInt32(command.ExecuteScalar());
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
            command.Parameters.AddWithValue("@MaDanhMuc", danhMuc.MaDanhMuc);
            command.Parameters.AddWithValue("@TenDanhMuc", danhMuc.TenDanhMuc);
            

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

        //  Hàm ánh xạ dữ liệu từ SQL -> Model
        private DanhMuc MapToDanhMuc(SqlDataReader reader)
        {
            return new DanhMuc
            {
                MaDanhMuc = reader.GetInt32(reader.GetOrdinal("MaDanhMuc")),
                TenDanhMuc = reader.GetString(reader.GetOrdinal("TenDanhMuc")),              
            };
        }
    }
}
