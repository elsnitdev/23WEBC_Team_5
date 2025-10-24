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
    public async Task<ActionResult<IEnumerable<object>>> GetAll(int? itemsPerPage = null)
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

        query.OrderByDescending(sp => sp.ThoiGianTao);

        if (itemsPerPage.HasValue && itemsPerPage.Value > 0)
        {
            query = query.Take(itemsPerPage.Value);
        }

        return await query.ToListAsync();
    }

    public async Task<SanPham> Add(SanPham sanPhamMoi, List<int> danhMucIds)
    {
        sanPhamMoi.ThoiGianTao = DateTime.UtcNow;
        sanPhamMoi.ThoiGianCapNhat = DateTime.UtcNow;
        sanPhamMoi.DanhMuc = new List<DanhMuc>();

        if(danhMucIds != null && danhMucIds.Any())
        {
            var dsDanhMuc = await _context.DanhMuc
                .Where(dm => danhMucIds.Contains(dm.MaDM))
                .ToListAsync();

            sanPhamMoi.DanhMuc = dsDanhMuc;
        }

        _context.SanPham.Add(sanPhamMoi);

        await _context.SaveChangesAsync();

        return sanPhamMoi;
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
