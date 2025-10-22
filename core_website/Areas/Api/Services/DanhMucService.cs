using core_website.Areas.Api.Models;
using core_website.Areas.Api.Services;
using Microsoft.Data.SqlClient;
using System.Data;

namespace core_website.Areas.Admins.Services
{
    public class DanhMucService : IDanhMucService
    {
        private readonly ILogger<DanhMucService> _logger;

        public DanhMucService(
          ILogger<DanhMucService> logger
        )
        {
            _logger = logger;
        }

        // Lấy tất cả danh mục
        public List<DanhMuc> GetAll()
        {
            return null;
        }

        //  Lấy 1 danh mục theo ID
        public DanhMuc? GetById(int id)
        {
            return null;
        }

        // Thêm mới danh mục
        public DanhMuc Add(DanhMuc danhMuc)
        {
            //const string checkQuery = @"
            //    SELECT TOP 1 MaDM FROM DanhMuc WHERE TenDM = @TenDanhMuc
            //";
            //const string query = @"
            //    INSERT INTO DanhMuc (TenDM)
            //    VALUES (@TenDanhMuc);
            //    SELECT SCOPE_IDENTITY();
            //";
            return danhMuc;
        }

        // Cập nhật danh mục
        public void Update(DanhMuc danhMuc)
        {
            //const string query = @"
            //        UPDATE DanhMuc
            //        SET TenDM = @TenDanhMuc,
            //        WHERE MaDM = @MaDanhMuc
            //    ";
        }

        //  Xóa danh mục
        public void Delete(int id)
        {
            //const string query = "DELETE FROM DanhMuc WHERE MaDM = @MaDanhMuc";
        }
        public void Categorize(int MaSP, int MaDM)
        {
            if (MaSP == null || MaDM == null)
            {
                throw new ArgumentException("Danh sách MaSP và MaDM không được rỗng.");
            }


            //// Validate MaSP
            //var sanPhamQuery = $"SELECT COUNT(*) FROM SanPham WHERE MaSP = {MaSP}";

            //Thao tác

            // Validate DSMaDM
            //var danhMucQuery = $"SELECT COUNT(*) FROM DanhMuc WHERE MaDM = {MaDM}";

            //Thao tác



            //var phanLoaiQuery = $"SELECT COUNT(*) FROM PhanLoai WHERE MaSP = {MaSP} AND MaDM = {MaDM}";
            //

            //var insertQuery = $"INSERT INTO PhanLoai (MaSP, MaDM) VALUES ({MaSP}, {MaDM})";
            //
        }
    }
}