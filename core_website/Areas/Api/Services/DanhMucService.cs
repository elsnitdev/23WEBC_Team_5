using core_website.Areas.Api.Models;
using core_website.Areas.Api.Services;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
namespace core_website.Areas.Admins.Services
{
    public class DanhMucService : IDanhMucService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<DanhMucService> _logger;

        public DanhMucService(
          ILogger<DanhMucService> logger
        )
        {
            _logger = logger;
        }

        // Lấy tất cả danh mục
        public async Task<ActionResult<IEnumerable<DanhMuc>>> GetAll()
        {
            var query =_context.DanhMuc.Select(dm => new DanhMuc
            {
                MaDM = dm.MaDM,
                TenDM = dm.TenDM
            });
            return await query.ToListAsync();
        }

        //  Lấy 1 danh mục theo ID
        public DanhMuc? GetById(int id)
        {
            var query = _context.DanhMuc
                .Where(dm => dm.MaDM == id)
                .Select(dm => new DanhMuc
                {
                    MaDM = dm.MaDM,
                    TenDM = dm.TenDM
                });
            return query.FirstOrDefault();//Tin: Lay ra danh muc dau tien tim duoc 
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
            var temp_dm= _context.DanhMuc
                .FirstOrDefault(dm => dm.TenDM == danhMuc.TenDM); // kiem tra da co danh muc hay chua
            if (temp_dm != null)
            {
                throw new ArgumentException("Danh mục đã tồn tại.");
            }
            _context.DanhMuc.Add(danhMuc); // them vao danh muc
            _context.SaveChanges();// luu thay doi 

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
            var query = _context.DanhMuc
                .FirstOrDefault(dm => dm.MaDM == danhMuc.MaDM);
            query.TenDM = danhMuc.TenDM;
            _context.SaveChanges();
        }

        //  Xóa danh mục
        public void Delete(int id)
        {
            //const string query = "DELETE FROM DanhMuc WHERE MaDM = @MaDanhMuc";
            var query = _context.DanhMuc
                .FirstOrDefault(dm => dm.MaDM == id);
            _context.DanhMuc.Remove(query);
            _context.SaveChanges();
        }
        public void Categorize(int MaSP, int MaDM)
        {
            if (MaSP == null || MaDM == null)
            {
                throw new ArgumentException("Danh sách MaSP và MaDM không được rỗng.");
            }

            var sanphamQuery=_context.SanPham.Select(sp=>sp.MaSP==MaSP).Count();
            if(sanphamQuery==0)
            {
                throw new ArgumentException("Mã sản phẩm không tồn tại.");
            }
            var danhmucQuery=_context.DanhMuc.Select(dm=>dm.MaDM==MaDM).Count();
            if(danhmucQuery==0)
            {
                throw new ArgumentException("Mã danh mục không tồn tại.");
            }
            var phanloaiQuery=_context.PhanLoai
                .Select(pl=>pl.MaSP==MaSP&&pl.MaDM==MaDM).Count();
            if(phanloaiQuery>0)
            {
                throw new ArgumentException("Sản phẩm đã được phân loại trong danh mục này.");
            }
            _context.PhanLoai.Add(new PhanLoai
            {
                MaSP=MaSP,
                MaDM=MaDM
            });
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