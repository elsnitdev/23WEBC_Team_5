// KhoaTr - 5/10/2025: Sửa lại namespace từ core_w2 thành core_website
using core_website.Areas.Api.Models;
using core_website.Areas.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Threading.Tasks;
namespace core_website.Services;
// KhoaTr - END

public class SanPhamService : ISanPhamService
{
    private readonly ApplicationDbContext _context;
    public SanPhamService(IConfiguration configuration, ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<object>> GetAll(int? itemsPerPage = null)
    {
        try
        {
            var query = _context.SanPham
            .Include(sp => sp.DanhMuc)
            .Select(sp => new
            {
                sp.MaSP,
                sp.TenSP,
                sp.DonGia,
                sp.KhuyenMai,
                sp.MoTa,
                sp.ThongSo,
                sp.SoLuong,
                sp.HinhAnh,
                sp.ThoiGianTao,
                sp.ThoiGianCapNhat,
                sp.TrangThai,
                DanhMuc = sp.DanhMuc.Select(dm => dm.TenDM).ToList()
            }

            );

            query = query.OrderByDescending(sp => sp.ThoiGianTao);

            if (itemsPerPage.HasValue && itemsPerPage.Value > 0)
            {
                query = query.Take(itemsPerPage.Value);
            }

            return await query.ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Lỗi khi Get All Sản phẩm: {ex.Message}");
        }
    }

    public async Task<SanPham> Add(SanPham sanPhamMoi, List<int> danhMucIds)
    {
        try
        {
            sanPhamMoi.ThoiGianTao = DateTime.UtcNow;
            sanPhamMoi.ThoiGianCapNhat = DateTime.UtcNow;
            sanPhamMoi.DanhMuc = new List<DanhMuc>();

            if (danhMucIds != null && danhMucIds.Any())
            {
                var dsDanhMuc = await _context.DanhMuc
                    .Where(dm => danhMucIds.Contains(dm.MaDM))
                    .ToListAsync();

                sanPhamMoi.DanhMuc = dsDanhMuc;
            }
            else
            {
                throw new Exception("Không có danh sách mã danh mục cho việc thêm sản phẩm");
            }

            _context.SanPham.Add(sanPhamMoi);

            await _context.SaveChangesAsync();

            return sanPhamMoi;
        }
        catch (Exception ex)
        {
            throw new Exception($"Lỗi trong lúc thêm sản phẩm: {ex.Message}");
        }
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
    public async Task<SanPham?> GetById(int id)
    {
        var sanPham = await _context.SanPham.FirstOrDefaultAsync(s => s.MaSP == id);
        if (sanPham == null) { 
            throw new Exception($"Không tìm thấy sản phẩm có mã: {id}");
        }
        return sanPham;
    }

    public List<SanPham> Search(string keyword)
    {
    return null;
    }

    public async Task<SanPham> UpdateImage(int MaSP, string imagePaths) {
        try
        {
            var sanPham = await _context.SanPham.FirstOrDefaultAsync(s => s.MaSP == MaSP);
            if (sanPham == null)
            {
                throw new Exception($"Không tìm thấy sản phẩm có mã: {MaSP}");
            }
            sanPham.HinhAnh = imagePaths;
            _context.Update(sanPham);
            await _context.SaveChangesAsync();
            return sanPham;
        }
        catch (Exception ex)
        {
            throw new Exception($"Lỗi trong lúc cập nhật đường dẫn hình ảnh: {ex.Message}");
        }
    }

    public async void Delete(int id) {
        try
        {
            var sanPham = await _context.SanPham.FirstOrDefaultAsync(s => s.MaSP == id);
            if (sanPham == null)
            {
                throw new Exception($"Không tìm thấy sản phẩm có mã: {id}");
            }
            sanPham.TrangThai = false;
            _context.Update(sanPham);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex) 
        {
            throw new Exception($"Lỗi trong lúc xoá sản phẩm: {ex.Message}");
        }
    }

    public List<SanPham> GetKhuyenMai()
    {
    return null;
    }
    //Huy end
}
